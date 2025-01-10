using System.Collections;
using DiscordWebhook;
using Newtonsoft.Json;
using UnityEngine;

namespace LogsProtocolAdvanced
{
    public class Utils
    {

        public static IEnumerator sendWebhook(EventConfig config, object[] args)
        {
            if (config == null)
            {
                Debug.LogError("EventConfig is null.");
                yield break;
            }

            Debug.Log($"Sending webhook to URL: {config.url}");
            if(config.webhook == null)
            {
                Debug.LogError("Webhook is null.");
                yield break;
            }
            Debug.Log(args.Length);
            if (config.webhook.content != null)
                config.webhook.content = string.Format(config.webhook.content, args);
            if(config.webhook.embeds != null)
            {
                foreach (var embed in config.webhook.embeds)
                {
                    if(embed.fields != null )
                    {
                        foreach (var field in embed.fields)
                        {
                            field.FormatValue(args);
                        }
                    }
                    embed.ConvertColor();
                    embed.FormatDescription(args);
                }
            }

            Debug.Log("Webhook message: " + config.webhook.content);
            string json = JsonConvert.SerializeObject(config.webhook);
            Debug.Log("Webhook JSON: " + json);
            WebhookSender.Send(config.webhook, config.url);
            yield return null;
        }
    }
}
