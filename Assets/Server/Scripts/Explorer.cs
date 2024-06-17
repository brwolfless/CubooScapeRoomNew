using Server.Scripts.JsonObjects;
using UnityEngine;

namespace Server.Scripts
{
    [System.Serializable]
    public static class Explorer
    {
        static Explorer()
        {
            ExploradorObject = new ExploradorObject();
            ServerEvents.OnSendExplorer += OnSendExplorerHandler;
            ServerEvents.OnDisconnectEvents += OnDisconnectEventsHandler;
            
        }
        public static ExploradorObject ExploradorObject;

        public static WebSocketIOController WebSocketIOController = null;

        private static void OnDisconnectEventsHandler()
        {
            ServerEvents.OnSendExplorer -= OnSendExplorerHandler;
            ServerEvents.OnDisconnectEvents -= OnDisconnectEventsHandler;
        }
        private static void OnSendExplorerHandler()
        {
            if (WebSocketIOController == null)
            {
                Debug.LogError("WebSocktIOController Missing from scene");
                return;
            }
            WebSocketIOController.EmitEventAndJson(WebSocketIOController.ExplorerSendEvent, ExploradorObject);
        }



    }
}