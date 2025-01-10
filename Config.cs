using System.Collections.Generic;
using DiscordWebhook;

public class Config
{
    public Dictionary<string, EventConfig> events { get; set; } = new Dictionary<string, EventConfig>();
    public static Config GetDefault()
    {
        var webhook = new WebhookBuilder("Join Event", null, null, "Default join event");
        return new Config
        {
            events = new Dictionary<string, EventConfig>
            {
                {
                    "onJoin", new EventConfig
                    {
                        url = "https://discord.com/api/webhooks/example",
                        webhook = webhook.Build()
                    }
                }
            }
        };
    }
}

public class EventConfig
{
    public string url { get; set; }
    public Webhook webhook { get; set; }
}
