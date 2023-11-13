using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

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

  public async static Task<T> FromJson<T>(string relativePath)
  {
    string json = await File.ReadAllTextAsync(GetPersistencePath(relativePath));
    return JsonConvert.DeserializeObject<T>(json);
  }
}