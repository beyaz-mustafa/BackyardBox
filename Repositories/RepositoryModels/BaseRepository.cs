using Entities.Models;
using System.Text.Json;

namespace Repositories.RepositoryModels
{
    // Base repository class for working with JSON-based storage of entities
    public abstract class BaseRepository<T> where T : Entity
    {
        // Application data path for storing repository files
        public static readonly string appPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BackyardBox");

        // Factory method to create an instance of T
        public abstract T Create(params object[] args);

        // Find an entity by id (supports full length or first 6 characters)
        public T? Find(string id)
        {
            // Look for a file that starts with the given id
            string filePath = Directory.GetFiles(appPath, $"{id}*.*", SearchOption.AllDirectories)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(filePath))
            {
                // Read JSON and deserialize into T
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            return null;
        }

        // Update an existing entity by overwriting its JSON file
        public void Update(T t)
        {
            string jsonString = JsonSerializer.Serialize(t);

            // Find the file corresponding to this entity's Id
            string filePath = Directory.GetFiles(appPath, $"{t.Id}*", SearchOption.AllDirectories)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(filePath))
            {
                File.WriteAllText(filePath, jsonString);
            }
        }

        // Create a new JSON file for an entity at a specific path
        public void CreateJsonFile(T t, string path)
        {
            string jsonString = JsonSerializer.Serialize(t);

            // Ensure directory exists before writing
            Directory.CreateDirectory(path);

            // Write entity as JSON file using Id as filename
            File.WriteAllText(Path.Combine(path, $"{t.Id}.json"), jsonString);
        }
    }
}
