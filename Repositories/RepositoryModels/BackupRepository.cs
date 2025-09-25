using Entities.Models;

namespace Repositories.RepositoryModels
{
    // Repository class for managing Backup entities
    public class BackupRepository : BaseRepository<Backup>
    {
        SaveRepository _saveRepository;

        public BackupRepository()
        {
            _saveRepository = new();
        }

        // Create a new backup and save it as a JSON file
        public override Backup Create(params object[] args)
        {
            /* Args contains :
             * args[0] : path (string)
             * args[1] : contents (List<string>)
             * args[2] : destination paths (List<string>)             
             */
            Backup backup = new();

            // First argument is the path where the backup folder will be created
            string path = Path.Combine((string)args[0], backup.Id);

            // Initialize backup properties from args
            backup.Contents = (List<string>)args[1];
            backup.DestinationPaths = (List<string>)args[2];

            // Add to FileSystemWatcher filters
            foreach (var item in backup.Contents)
            {
                backup.FileWatcher.Filters.Add(item);
            }

            // Save backup as JSON in its dedicated folder
            CreateJsonFile(backup, path);
            return backup;
        }
        
        // Delete specified backups from a given path
        public void Delete(string path, HashSet<string> backups)
        {
            string[] dirs = Directory.GetDirectories(path);

            // Delete directories whose names match backup Ids
            foreach (var item in dirs)
            {
                if (backups.Contains(new DirectoryInfo(item).Name))
                    Directory.Delete(item, true); // Recursive delete
            }
        }

        // Add a new save to an existing backup
        public void AddSave(string id)
        {
            Backup backup = Find(id);
            if (backup == null) return;

            // Find the backup folder path
            string path = Directory.GetDirectories(appPath, id.ToString(), SearchOption.AllDirectories)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(path)) return;

            // Create a new save and add its Id to the backup's Saves list
            backup.Saves.Add(_saveRepository.Create(path).Id);

            // Update backup JSON to reflect the added save
            Update(backup);
        }
    }
}
