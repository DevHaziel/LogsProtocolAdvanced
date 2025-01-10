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

            if (config.webhook == null)
            {
                Debug.LogError("Webhook is null.");
                yield break;
            }

            // Crear una copia del webhook para evitar mutaciones no deseadas
            var webhookCopy = JsonConvert.DeserializeObject<Webhook>(JsonConvert.SerializeObject(config.webhook));

            // Formatear contenido y embeds
            if (webhookCopy.content != null)
                webhookCopy.content = string.Format(webhookCopy.content, args);

            if (webhookCopy.embeds != null)
            {
                foreach (var embed in webhookCopy.embeds)
                {
                    if (embed.fields != null)
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

            string json = JsonConvert.SerializeObject(webhookCopy);

            // Enviar el webhook
            WebhookSender.Send(webhookCopy, config.url);
            yield return null;
        }
    }
}
