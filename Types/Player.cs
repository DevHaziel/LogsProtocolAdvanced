using BrokeProtocol.API;
using BrokeProtocol.Entities;
using UnityEngine;

namespace LogsProtocolAdvanced.Types
{
    public class Player : PlayerEvents
    {
        [Execution(ExecutionMode.PostEvent)]
        public override bool Initialize(ShEntity entity)
        {
            if (entity is ShPlayer player)
            {
                if (!player.isHuman) return true;
                if (Files.Instance.config == null)
                {
                    Debug.LogError("Config is null.");
                    return true;
                }
                if(!Files.Instance.config.events.ContainsKey("onJoin"))
                {
                    return true;
                }
                EventConfig config = Files.Instance.config.events["onJoin"];
                if (string.IsNullOrEmpty(config.url))
                    return true;
                player.StartCoroutine(Utils.sendWebhook(config, new object[] { player.username, player.ID }));
            }
            return true;
        }
        [Execution(ExecutionMode.PostEvent)]
        public override bool Destroy(ShEntity entity)
        {
            if (entity is ShPlayer player)
            {
                if (!player.isHuman) return true;
                if (Files.Instance.config == null)
                {
                    Debug.LogError("Config is null.");
                    return true;
                }
                if (!Files.Instance.config.events.ContainsKey("onLeft"))
                {
                    return true;
                }
                EventConfig config = Files.Instance.config.events["onLeft"];
                if (string.IsNullOrEmpty(config.url))
                    return true;
                player.StartCoroutine(Utils.sendWebhook(config, new object[] { player.username, player.ID }));
            }
            return true;
        }
        [Execution(ExecutionMode.PostEvent)]
        public override bool Death(ShDestroyable destroyable, ShPlayer attacker)
        {
            if (destroyable is ShPlayer player)
            {
                if (attacker == null) attacker = player;
                if(!player.isHuman) return true;
                if (Files.Instance.config == null)
                {
                    Debug.LogError("Config is null.");
                    return true;
                }
                if (!Files.Instance.config.events.ContainsKey("onDeath"))
                {
                    return true;
                }
                EventConfig config = Files.Instance.config.events["onDeath"];
                if (string.IsNullOrEmpty(config.url))
                    return true;
                player.StartCoroutine(Utils.sendWebhook(config, new object[] { player.username, player.ID, attacker.username, attacker.ID }));
                if (Files.Instance.config.events.ContainsKey("onKill"))
                {
                    EventConfig config2 = Files.Instance.config.events["onKill"];
                    if (string.IsNullOrEmpty(config2.url))
                        return true;
                    attacker.StartCoroutine(Utils.sendWebhook(config2, new object[] {  attacker.username, attacker.ID, player.username, player.ID, }));
                }
            }
            return true;
        }
        [Execution(ExecutionMode.PostEvent)]
        public override bool ChatLocal(ShPlayer player, string message)
        {
            if (Files.Instance.config == null)
            {
                Debug.LogError("Config is null.");
                return true;
            }
            if (!Files.Instance.config.events.ContainsKey("onChatLocal"))
            {
                return true;
            }
            EventConfig config = Files.Instance.config.events["onChatLocal"];
            if (string.IsNullOrEmpty(config.url))
                return true;
            player.StartCoroutine(Utils.sendWebhook(config, new object[] { player.username, player.ID, message }));
            return true;
        }
        [Execution(ExecutionMode.PostEvent)]
        public override bool ChatGlobal(ShPlayer player, string message)
        {
            if (Files.Instance.config == null)
            {
                Debug.LogError("Config is null.");
                return true;
            }
            if (!Files.Instance.config.events.ContainsKey("onChatGlobal"))
            {
                return true;
            }
            EventConfig config = Files.Instance.config.events["onChatGlobal"];
            if (string.IsNullOrEmpty(config.url))
                return true;
            player.StartCoroutine(Utils.sendWebhook(config, new object[] { player.username, player.ID, message }));
            return true;
        }
        [Execution(ExecutionMode.PostEvent)]
        public override bool Command(ShPlayer player, string message)
        {
            if (Files.Instance.config == null)
            {
                Debug.LogError("Config is null.");
                return true;
            }
            if (!Files.Instance.config.events.ContainsKey("onCommand"))
            {
                return true;
            }
            EventConfig config = Files.Instance.config.events["onCommand"];
            if (string.IsNullOrEmpty(config.url))
                return true;
            player.StartCoroutine(Utils.sendWebhook(config, new object[] { player.username, player.ID, message }));
            return true;
        }
    }
}
