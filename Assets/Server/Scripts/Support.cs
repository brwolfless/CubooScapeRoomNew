using Server.Scripts.JsonObjects;
using UnityEngine;

namespace Server.Scripts
{
    [System.Serializable]
    public static class Support
    {
        static Support()
        {
            SuporteObject = new SuporteObject();
            ServerEvents.OnSendSupport += OnSendSupportHandler;
            ServerEvents.OnDisconnectEvents += OnDisconnectEventsHandler;
        }

        private static void OnDisconnectEventsHandler()
        {
            ServerEvents.OnSendSupport -= OnSendSupportHandler;
            ServerEvents.OnDisconnectEvents -= OnDisconnectEventsHandler;
        }

        public static SuporteObject SuporteObject;

        public static WebSocketIOController WebSocketIOController = null;

        private static void OnSendSupportHandler()
        {
            if (WebSocketIOController == null)
            {
                Debug.LogError("WebSocktIOController Missing from scene");
                return;
            }
            WebSocketIOController.EmitEventAndJson(WebSocketIOController.SupportSendEvent, SuporteObject);
        }



    }
}