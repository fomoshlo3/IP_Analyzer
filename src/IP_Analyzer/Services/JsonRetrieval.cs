using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IP_Analyzer.Services
{
    public static class JsonRetrieval
    {
        private static string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , Assembly.GetCallingAssembly().GetName().Name!.ToLower());

        private const string HISTORY_FILE_NAME = "NetworkInfoHistory.json";

        private static readonly JsonSerializerOptions _options = new()
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
        };

        public static IEnumerable<NetworkInfo> Load()
        {
            string historyFilePath = Path.Combine(AppDataPath, HISTORY_FILE_NAME);

            string json = File.ReadAllText(historyFilePath);

            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("No data found in the history file.");
            }
            else
            {
                ICollection<NetworkInfo> data = [];
                var savedHistory = JsonSerializer.Deserialize<IEnumerable<NetworkInfoDTO>>(json, _options) ?? throw new Exception("NetworkInfo could not be deserialized.");
                
                foreach(var dto in savedHistory)
                {
                    var networkInfo = dto.MapToNetworkInfo();
                    data.Add(networkInfo);
                }
                return data;
            }
        }
    }
}
