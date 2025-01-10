using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
namespace LogsProtocolAdvanced
{
    public class Files
    {
        public Config config { get; private set; } = new Config();
        private static Files _instance;
        private static readonly object _lock = new object();
        private readonly string ConfigDirectory = Path.Combine("LogsProtocol Advanced");
        private readonly string ConfigFilePath = Path.Combine("LogsProtocol Advanced", "config.json");

        public static Files Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Files();
                        }
                    }
                }
                return _instance;
            }
        }

        private Files()
        {
            LoadConfig();
            ReloadConfig();
        }

        public void ReloadConfig()
        {
            try
            {
                Debug.Log("Reloading config...");
                var json = File.ReadAllText(ConfigFilePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    Debug.LogError("Config file is empty. Loading default configuration.");
                    config = Config.GetDefault();
                }
                else
                {
                    config = JsonConvert.DeserializeObject<Config>(json);
                }
                if (config == null)
                {
                    Debug.LogError("Deserialized config is null. Reverting to default config.");
                    config = Config.GetDefault();
                }
                Debug.Log("Config reloaded.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to reload config: {ex.Message}");
                config = Config.GetDefault(); // Asegurarse de que siempre haya un valor por defecto.
            }
        }

        public void LoadConfig()
        {
            try
            {
                Debug.Log("Loading config...");
                if (!Directory.Exists(ConfigDirectory))
                {
                    Directory.CreateDirectory(ConfigDirectory);
                    Debug.Log("Directory created.");
                }

                if (!File.Exists(ConfigFilePath))
                {
                    var json = JsonConvert.SerializeObject(Config.GetDefault(), Formatting.Indented);
                    File.WriteAllText(ConfigFilePath, json);
                    Debug.Log("[LogsProtocol Advanced] Config file created.");
                }
                else
                {
                    Debug.Log("Config file already exists.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load config: {ex.Message}");
            }
        }
    }
}
