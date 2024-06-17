using Newtonsoft.Json;

namespace Server.Scripts.JsonObjects
{
    public class SuporteObject
    {
        public string gameId;
        public ESGPoints esgPoints = new ESGPoints();
        public string eventName;
        public string message;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ESGPoints
    {
        public int E;
        public int S;
        public int G;
    }
}