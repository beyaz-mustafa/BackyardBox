using Entities.Models;
using Repositories.RepositoryModels;
using System.Globalization;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Repositories.Concretes
{
    public class BackupRepository : BaseRepository<Backup>
    {
        SaveRepository _saveRepository;
        public BackupRepository(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;
        }

        public override Backup Create(params object[] args)
        {
            Backup backup = new();
            string path = Path.Combine((string)args[0], backup.Id.ToString());

            foreach (var item in args.Skip(1))
            {
                backup.Contents.Add((string)item);
                backup.FileWatcher.Filters.Add((string)item);
            }

            CreateJsonFile(backup, path);
            return backup;
        }

        public void Delete(string path, HashSet<string> backups)
        {
            string[] dirs = Directory.GetDirectories(path);
            foreach(var item in dirs)
            {
                if(backups.Contains(new DirectoryInfo(item).Name))
                    Directory.Delete(item, true);
            }
        }

        public void AddSave(string id)
        {
            Backup backup = Find(id);
            if (backup == null) return;

            string path = Directory.GetDirectories(appPath, id.ToString(), SearchOption.AllDirectories)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(path)) return;

            backup.Saves.Add(_saveRepository.Create(path).Id);
            Update(backup);
        }
    }
}
