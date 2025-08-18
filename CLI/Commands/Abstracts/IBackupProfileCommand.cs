using Entities.Models;

namespace CLI.Commands.Abstracts
{
    public interface IBackupProfileCommand : ICommand<BackupProfile>
    {
        public void UpdateAlias(int id, string alias);
        public void AddBackup(int id, Backup backup);
        public void AddBackup(int id, List<Backup> backups);
        public void RemoveBackup(int id, int backupId);
        public void RemoveBackup(int id, List<int> backupId);
        public void StartSync(int id);
        public void StopSync(int id);
        public void UpdateMaxSave(int id, int maxSave);
    }
}
