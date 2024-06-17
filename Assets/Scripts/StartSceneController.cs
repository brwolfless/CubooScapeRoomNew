using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Server;
using Server.Scripts;
using System;
using TMPro;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    [Header("UI")]
    public Uri uri = new Uri("wss://gdt-websocketserver.onrender.com/");
    [Header("OnValidate")]
    [SerializeField] private string editor_id;
    [SerializeField] private bool editor_connectToServer;
    [SerializeField] private bool editor_connectToGame;

    private void OnValidate()
    {
        if (editor_connectToServer)
        {
            editor_connectToServer = false;
            //InputIP(editor_id);
            ConnectToServer();
        }

        if (editor_connectToGame)
        {
            editor_connectToGame = false;
            InputGameID(editor_id);
            ConnectToGame();
        }
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;
    }
    /// <summary>
    /// Usar num botao
    /// </summary>
    public void ConnectToServer()
    {
        //webSocketIOController.ConnectToServer();
        WebSocketIOController.Instance.ConnectToServer();
    }
    public void UpdateIP()
    {
        //webSocketIOController.SetURI(uri);
        WebSocketIOController.Instance.SetURI(uri);
        ConnectToServer();
    }
    /// <summary>
    /// Input via inputfield
    /// </summary>
    /// <param name="ip"></param>
    public void InputIP(string ip)
    {
        uri = new System.Uri(ip);
    }

    /// <summary>
    /// Chamar por um botao
    /// </summary>
    public void ConnectToGame()
    {
        //webSocketIOController.GameInfo.role = WebSocketIOController.ExplorerName;
        //webSocketIOController.ConnetToGame();
        WebSocketIOController.Instance.GameInfo.role = WebSocketIOController.ExplorerName;
        WebSocketIOController.Instance.ConnetToGame();
    }
    /// <summary>
    /// Receber por um inputfield
    /// </summary>
    /// <param name="id"></param>
    public void InputGameID(string id)
    {
        //webSocketIOController.GameInfo.gameId = id.ToUpper();
        WebSocketIOController.Instance.GameInfo.gameId = id.ToUpper();
    }
}
