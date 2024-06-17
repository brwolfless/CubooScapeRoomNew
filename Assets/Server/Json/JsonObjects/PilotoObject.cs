using Newtonsoft.Json;

namespace Server.Scripts.JsonObjects
{
    public class PilotoObject
    {
        public string gameId;
        public DronePos position = new DronePos();
        public int localType;
        public string eventName;
        public string message;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class DronePos
    {
        public float X;
        public float Y;
    }

    public enum DronePossiblePositions
    {
        OpenAir,
        Moving,
        Biofrost,
        Shield,
        Charger
    }
    
}