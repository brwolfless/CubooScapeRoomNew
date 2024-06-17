using System;
using System.Collections.Generic;
using Best.SocketIO;
using Best.SocketIO.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using Server.Json.JsonObjects;
using Server.Scripts.JsonObjects;
using TMPro;

namespace Server.Scripts
{
public class WebSocketIOController : MonoBehaviour
{
    public const string PilotName = "pilot";
    public const string ExplorerName = "explorer";
    public const string SupportName = "support";

    public const string ConnectToGameEvent = "connectToGame";
    public const string PilotSendEvent = "onPilotEvent";
    public const string ExplorerSendEvent = "onExplorerEvent";
    public const string SupportSendEvent = "onSupportEvent";
    public const string PilotReceivedEvent = "pilotUpdate";
    public const string ExplorerReceivedEvent = "explorerUpdate";
    public const string SupportReceivedEvent = "supportUpdate";
    public const string GameMasterReceivedEvent = "onGameMasterEvent";
    public const string ErrorReceivedEvent = "error";
    public const string ConnectedToGameEvent = "connectedToGame";
    public const string GameStartedEvent = "gameStarted";
    public const string GameEndedEvent = "gameEnded";
    private SocketManager _socketManager;

    public ConnectObject GameInfo;
    
    public string ReceivedText;

    private Uri _uri = new Uri("wss://gdt-websocketserver.onrender.com/");

    public string GameID;
    public static WebSocketIOController Instance;
    private bool _isOpen;

    private bool _isConnecting;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool IsOpen()
    {
        return _isOpen;
    }

    public string GetURI()
    {
        return _uri.ToString();
    }
    public void SetURI(Uri uri)
    {
        _uri = uri;
    }

    public void ConnetToGame()
    {
        _socketManager.Socket.Emit(ConnectToGameEvent, GameInfo);
        Debug.Log(GameInfo.ToString());
    }
    public void ConnectToServer()
    {

        if (_socketManager == null)
        {
            _isConnecting = true;
            SocketOptions socketOptions = new SocketOptions();
            _socketManager = new SocketManager(_uri);
            _socketManager.Socket.On<ConnectResponse>(SocketIOEventTypes.Connect, OnConnect);
            _socketManager.Socket.On<Error>(SocketIOEventTypes.Error, OnError);
            _socketManager.OnIncomingPacket += OnIncomingPacketHandler;
            _socketManager.Open();
            return;
        }

        if (_isConnecting)
        {
            _isConnecting = false;
            _socketManager.Close();
            _socketManager = null;
            ConnectToServer();
        }
    }
    void Start()
    {
        ControllerEvents.OnInputWsUrl += OnInputWsUrlHandler;
        ServerEvents.OnReceivedTextFromServer += OnReceivedTextFromServerHandler;
        Pilot.WebSocketIOController = this;
        Support.WebSocketIOController = this;
        Explorer.WebSocketIOController = this;
    }

    private void OnReceivedTextFromServerHandler(string arg1, string arg2)
    {
        Debug.Log($"Received:{arg1},{arg2}.");
    }

    private void OnIncomingPacketHandler(SocketManager arg1, IncomingPacket arg2)
    {
        if (arg2.EventName == "ping"|| arg2.EventName=="pong"||arg2.EventName =="open"||arg2.EventName=="connect")
        {
            return;
        }
            
        var amount = arg2.EventName.Length +3;
        arg2.Payload =arg2.Payload.Remove(0, amount);
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            arg2.Payload =arg2.Payload.Remove(0,1);
            arg2.Payload =arg2.Payload.Remove(arg2.Payload.Length-1,1);
            ServerEvents.OnReceivedTextFromServer.Invoke(arg2.EventName, arg2.Payload);
            
            
            switch (arg2.EventName)
            {
                case PilotReceivedEvent:
                    ServerEvents.OnReceivedPilot?.Invoke(JsonConvert.DeserializeObject<ReceivedPiloto>(arg2.Payload));
                    break;
                case ExplorerReceivedEvent:
                    ServerEvents.OnReceivedExplorer?.Invoke(JsonConvert.DeserializeObject<ReceivedExplorador>(arg2.Payload));
                    break;
                case SupportReceivedEvent:
                    ServerEvents.OnReceivedSupport?.Invoke(JsonConvert.DeserializeObject<ReceivedSuporte>(arg2.Payload));
                    break;
                case GameMasterReceivedEvent:
                    ServerEvents.OnReceivedGameMaster?.Invoke(JsonConvert.DeserializeObject<ReceivedGameMaster>(arg2.Payload));
                    break;
                case ErrorReceivedEvent:
                    ServerEvents.OnServerError?.Invoke(JsonConvert.DeserializeObject<ErrorObject>(arg2.Payload).message);
                    break;
                case ConnectedToGameEvent:
                    ServerEvents.OnGameConnected?.Invoke();
                    break;
                case GameStartedEvent:
                    ServerEvents.OnGameStarted?.Invoke();
                    break;
                case GameEndedEvent:
                    ServerEvents.OnGameEnded?.Invoke();
                    break;
            }
                
        });
            
    }

    void OnConnect(ConnectResponse resp)
    {
        ServerEvents.OnServerConnected?.Invoke();
        _isOpen = true;
        Debug.Log("Connected!");
    }
        
    void OnError(Error error)
    {
        ServerEvents.OnServerError?.Invoke(error.message);
        Debug.Log($"An error occured: {error}");
    }


    private void OnInputWsUrlHandler(string obj)
    {
        _uri = new Uri(obj);
    }
    

    

    public void EmitEventAndJson(string eventName, object json)
    {
        _socketManager.Socket.Emit(eventName, json);
    }

    private void OnApplicationQuit()
    {
        ControllerEvents.OnInputWsUrl -= OnInputWsUrlHandler;
        ServerEvents.OnReceivedTextFromServer -= OnReceivedTextFromServerHandler;
        ServerEvents.OnDisconnectEvents.Invoke();
    }
}
}