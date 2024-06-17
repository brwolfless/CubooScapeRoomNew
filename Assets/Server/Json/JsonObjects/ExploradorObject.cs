using Newtonsoft.Json;

namespace Server.Scripts.JsonObjects
{
    public class ExploradorObject
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