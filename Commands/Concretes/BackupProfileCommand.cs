using Commands.Abstracts;
using Entities.Models;

namespace Commands.Concretes
{
    public class BackupProfileCommand : IBackupProfileCommand
    {
        public void AddBackup(int id, Backup backup)
        {
            BackupProfile backupProfile = Find(id);
            backupProfile.Backups.Add(backup);
        }

        public void AddBackup(int id, List<Backup> backups)
        {
            BackupProfile backupProfile = Find(id);
            backupProfile.Backups.AddRange(backups);
        }

        public BackupProfile Find(int id)
        {
            // This method should retrieve the BackupProfile by id from a data source.
            // For now, we will throw an exception to indicate that this method is not implemented.
            throw new NotImplementedException("Find method is not implemented.");
        }

        public void RemoveBackup(int id, int backupId)
        {
            BackupProfile backupProfile = Find(id);
            var backupToRemove = backupProfile.Backups.FirstOrDefault(b => b.Id == backupId);
            backupProfile.Backups.Remove(backupToRemove);
        }

        public void RemoveBackup(int id, List<int> backupId)
        {
            BackupProfile backupProfile = Find(id);
            backupProfile.Backups.RemoveAll(b => backupId.Contains(b.Id));
        }

        public void StartSync(int id)
        {
            // This method should start the synchronization process for the backup profile.
            // For now, we will throw an exception to indicate that this method is not implemented.
            throw new NotImplementedException("StartSync method is not implemented.");
        }

        public void StopSync(int id)
        {
            // This method should stop the synchronization process for the backup profile.
            // For now, we will throw an exception to indicate that this method is not implemented.
            throw new NotImplementedException("StopSync method is not implemented.");
        }

        public void UpdateAlias(int id, string alias)
        {
            var backupProfile = Find(id);
            backupProfile.Alias = alias;
        }

        public void UpdateMaxSave(int id, int maxSave)
        {
            var backupProfile = Find(id);
            backupProfile.MaxSaves = maxSave;
        }
    }
}
