using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async void LoadConfig()
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
                    Debug.Log("Config file does not exist. Attempting to download...");

                    // Intentar descargar el config.json desde la URL
                    var configDownloaded = await DownloadConfigJsonAsync(ConfigFilePath);

                    // Si la descarga fue exitosa, no es necesario crear un archivo nuevo.
                    if (!configDownloaded)
                    {
                        // Si la descarga falla, crear el archivo con la configuración predeterminada
                        var json = JsonConvert.SerializeObject(Config.GetDefault(), Formatting.Indented);
                        File.WriteAllText(ConfigFilePath, json);
                        Debug.Log("[LogsProtocol Advanced] Config file created with default settings.");
                    }
                }
                else
                {
                    Debug.Log("Config file already exists.");
                }
                ReloadConfig();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load config: {ex.Message}");
            }
        }
        private async Task<bool> DownloadConfigJsonAsync(string filePath)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // URL de donde descargar el archivo
                    string configUrl = "https://raw.githubusercontent.com/DevHaziel/LogsProtocolAdvanced/master/Examples/config.json";

                    // Descargar el archivo
                    var response = await client.GetAsync(configUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        File.WriteAllText(filePath, json);
                        Debug.Log("[LogsProtocol Advanced] Config file downloaded successfully.");
                        return true; // Indica que la descarga fue exitosa
                    }
                    else
                    {
                        Debug.LogError($"Failed to download config: {response.ReasonPhrase}");
                        return false; // Indica que la descarga falló
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during config download: {ex.Message}");
                return false; // Si ocurre una excepción, la descarga falló
            }
        }
    }
}
