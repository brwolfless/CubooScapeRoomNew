using Server.Scripts.JsonObjects;
using UnityEngine;

namespace Server.Scripts
{
    [System.Serializable]
    public static class Pilot
    {
        static Pilot()
        {
            PilotObject = new PilotoObject();
            ServerEvents.OnSendPilot += OnSendPilotToServerHandler;
            ServerEvents.OnDisconnectEvents += OnDisconnectEventsHandler;
        }


        public static PilotoObject PilotObject;

        public static WebSocketIOController WebSocketIOController = null;
        
        
        public static void OnSendPilotToServerHandler()
        {
            if (WebSocketIOController == null)
            {
                Debug.LogError("WebSocktIOController Missing from scene");
                return;
            }
            WebSocketIOController.EmitEventAndJson(WebSocketIOController.PilotSendEvent, PilotObject);
        }
        
        private static void OnDisconnectEventsHandler()
        {
            ServerEvents.OnSendPilot -= OnSendPilotToServerHandler;
            ServerEvents.OnDisconnectEvents -= OnDisconnectEventsHandler;
            
        }
    }
}