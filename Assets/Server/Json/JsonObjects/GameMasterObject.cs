using Newtonsoft.Json;

namespace Server.Json.JsonObjects
{
    [System.Serializable]
    public class GameMasterObject
    {
        public string gameId;
        public string eventName;
        public string message;
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}