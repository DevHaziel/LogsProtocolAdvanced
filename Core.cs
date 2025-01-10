using BrokeProtocol.API;

namespace LogsProtocolAdvanced
{
    public class Core : Plugin
    {
        public Core()
        {
            Info = new PluginInfo("LogsProtocol Advanced", "logs")
            {
                Description = "LogsProtocol Advanced - Add discord webhooks to your events",
                Website = "https://haziel.xyz/",
            };
            Files.Instance.LoadConfig();
        }
    }
}
