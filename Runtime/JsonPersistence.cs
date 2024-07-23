using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Persistence
{
    public static class JsonPersistence
    {
        private static string GetPersistencePath(string relativePath)
        {
            return Path.Combine(Application.persistentDataPath, relativePath);
        }

        public static Task PersistJson<T>(T item, string relativePath)
        {
            string json = JsonConvert.SerializeObject(item);
            return File.WriteAllTextAsync(GetPersistencePath(relativePath), json);
        }

        public static async Task<T> FromJson<T>(string relativePath)
        {
            string json = await File.ReadAllTextAsync(GetPersistencePath(relativePath));
            return JsonConvert.DeserializeObject<T>(json);
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

