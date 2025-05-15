using System.Reflection;
using System.Text.Json;

namespace IP_Analyzer.Services
{
    public static class JsonStorage
    {
        // Hol den Pfad der Laufzeitumgebung für den speziellen Ordner Anwendungsdaten des aktuellen Benutzers
        private static string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Assembly.GetCallingAssembly().GetName().Name!.ToLower());

        private const string HISTORY_FILE_NAME = "NetworkInfoHistory.json";

        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
        };

        public static void CreateDirectory()
        {
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }
        }

        public static void Save(IEnumerable<NetworkInfo> infoHistory)
        {
            ICollection<NetworkInfoDTO> dtoList = [];
            foreach (var info in infoHistory)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info), "NetworkInfo cannot be null.");
                }
                var dto = info.MapToDTO();
                dtoList.Add(dto);
            }

            string json = JsonSerializer.Serialize(dtoList, _options);
            File.WriteAllText(Path.Combine(AppDataPath, HISTORY_FILE_NAME), json);
        }
    }
}
