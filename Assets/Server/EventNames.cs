namespace Server
{
    public static class EventNames
    {
        public const string DocumentDownloaded = "OnDocumentDownloaded";
        public const string BiforstActivated = "OnBifrostActivated";
        public const string AskingForBifrost = "OnAskingForBifrost";
        public const string RechargeBattery = "OnPilotRechargingBattery";
        public const string StartTempest = "OnTempestStarted";
        public const string EndTempest = "OnTempestEnded";
        public const string EndGame = "OnGameEnded";
        public const string ExitDrone = "OnExplorerExitDrone";
        public const string EnterDrone = "OnExplorerEnterDrone";
    }
}