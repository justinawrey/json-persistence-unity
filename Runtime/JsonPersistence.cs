using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Persistence
{
    public static class JsonPersistence
    {
        private static Task<string> ReadFromFile(string relativePath)
        {
            return File.ReadAllTextAsync(GetPersistencePath(relativePath));
        }

        public static string GetPersistencePath(string relativePath)
        {
            return Path.Combine(Application.persistentDataPath, relativePath);
        }

        public static Task PersistJson<T>(
            T item,
            string relativePath,
            params JsonConverter[] converters
        )
        {
            string json = JsonConvert.SerializeObject(item, converters);
            return File.WriteAllTextAsync(GetPersistencePath(relativePath), json);
        }

        public static async Task<T> FromJson<T>(
            string relativePath,
            JsonSerializerSettings settings
        )
        {
            return JsonConvert.DeserializeObject<T>(await ReadFromFile(relativePath), settings);
        }

        public static async Task<T> FromJson<T>(
            string relativePath,
            params JsonConverter[] converters
        )
        {
            return JsonConvert.DeserializeObject<T>(await ReadFromFile(relativePath), converters);
        }

        public static bool JsonExists(string relativePath)
        {
            return File.Exists(GetPersistencePath(relativePath));
        }

        public static void DeleteJson(string relativePath)
        {
            if (!JsonExists(relativePath))
            {
                return;
            }

            File.Delete(GetPersistencePath(relativePath));
        }
    }
}
