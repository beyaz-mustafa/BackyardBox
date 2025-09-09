using Entities.Models;
using Repositories.RepositoryModels;

namespace Repositories.Concretes
{
    public class ProfileRepository : BaseRepository<Profile>
    {
        BackupRepository _backupRepository;
        public ProfileRepository(BackupRepository backupRepository)
        {
            _backupRepository = backupRepository;
            FileSystemWatcher watcher = new("");
        }

        static readonly string ProfilesPath = Path.Combine(appPath, "Profiles");

        public override Profile Create(params object[] args)
        {
            Profile profile = new();
            CreateJsonFile(profile, Path.Combine(ProfilesPath, profile.Id.ToString()));
            return profile;
        }

        public void Delete(string id)
        {
            Profile profile = Find(id);
            if (profile == null) return;

            string dirPath = Directory.GetDirectories(ProfilesPath, id, SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (dirPath != null)
            {
                Directory.Delete(dirPath, true);
            }
        }

        public void AddBackup(string id, List<string> contents)
        {
            Profile profile = Find(id);
            if (profile == null) return;

            string path = Path.Combine(ProfilesPath, profile.Id.ToString(), "Backups");
            profile.Backups.Add(_backupRepository.Create(path, contents).Id);

            Update(profile);
        }

        public void RemoveBackup(string id, HashSet<string> backups)
        {
            Profile profile = Find(id);
            if (profile == null) return;

            profile.Backups.RemoveAll(b => backups.Contains(b));
            Update(profile);

            string backupsPath = Path.Combine(ProfilesPath, profile.Id.ToString(), "Backups");
            _backupRepository.Delete(backupsPath, backups);
        }
    }
}
