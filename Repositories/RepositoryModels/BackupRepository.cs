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
            Backup backup = new();

            // First argument is the path where the backup folder will be created
            string path = Path.Combine((string)args[0], backup.Id);

            // Remaining arguments are file/folder paths to include in the backup
            foreach (var item in args.Skip(1))
            {
                backup.Contents.Add((string)item);        // Add to backup contents list
                backup.FileWatcher.Filters.Add((string)item); // Add to FileSystemWatcher filters
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
