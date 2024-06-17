using System;
using Newtonsoft.Json;

namespace Server.Json.JsonObjects
{
    [Serializable]
    public class ConnectObject
    {
        public string gameId;
        public string role;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}