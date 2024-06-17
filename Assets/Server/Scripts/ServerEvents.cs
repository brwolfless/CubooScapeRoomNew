using System;
using Server.Json.JsonObjects;
using Server.Scripts.JsonObjects;

namespace Server.Scripts
{
    public static class ServerEvents
    {
        public static Action OnSendPilot;
        public static Action OnSendSupport;
        public static Action OnSendExplorer;
        public static Action OnDisconnectEvents;
        public static Action<ReceivedPiloto> OnReceivedPilot;
        public static Action<ReceivedSuporte> OnReceivedSupport;
        public static Action<ReceivedExplorador> OnReceivedExplorer;
        public static Action<ReceivedGameMaster> OnReceivedGameMaster;
        public static Action<string,string> OnReceivedTextFromServer;
        public static Action OnServerConnected;
        public static Action OnGameConnected;
        public static Action OnGameStarted;
        public static Action OnGameEnded;
        public static Action<string> OnServerError;
    }
}