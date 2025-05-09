﻿using System;
using BrokeProtocol.API;
using BrokeProtocol.Managers;
using UnityEngine;

namespace LogsProtocolAdvanced.Types
{
    public class Manager : ManagerEvents
    {
        [Execution(ExecutionMode.PostEvent)]
        public override bool Start()
        {
            if (Files.Instance.config == null)
            {
                Debug.LogError("Config is null.");
                return true;
            }
            if (!Files.Instance.config.events.ContainsKey("onStart"))
            {
                return true;
            }
            EventConfig config = Files.Instance.config.events["onStart"];
            if (string.IsNullOrEmpty(config.url))
                return true;
            SvManager.Instance.StartCoroutine(Utils.sendWebhook(config, new object[] {DateTime.Now.ToString("HH:mm:ss")}));
            return true;
        }
    }
}
