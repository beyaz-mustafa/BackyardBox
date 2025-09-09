using Entities.Models;
using System.Text.Json;

namespace Repositories.RepositoryModels
{
    public abstract class BaseRepository<T> where T : Entity
    {
        public static readonly string appPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BackyardBox");
        public abstract T Create(params object[] args);
        public T? Find(string id)
        {
            string filePath = Directory.GetFiles(appPath, $"{id}.*", SearchOption.AllDirectories)
                .FirstOrDefault();

            if(!string.IsNullOrEmpty(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            return null;
        }

        public void Update(T t)
        {
            string jsonString = JsonSerializer.Serialize(t);
            string filePath = Directory.GetFiles(appPath, $"{t.Id}*", SearchOption.AllDirectories)
                .FirstOrDefault();

            if(!string.IsNullOrEmpty(filePath))
            {
                File.WriteAllText(filePath, jsonString);
            }
        }

        public void CreateJsonFile(T t, string path)
        {
            string jsonString = JsonSerializer.Serialize(t);
            Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, $"{t.Id}.json"), jsonString);
        }
    }
}
