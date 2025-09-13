using Entities.Models;

namespace Repositories.RepositoryModels
{
    // Repository class for managing Profile entities
    public class ProfileRepository : BaseRepository<Profile>
    {
        BackupRepository _backupRepository;

        public ProfileRepository()
        {
            _backupRepository = new();
        }

        // Path to store profile data
        static readonly string ProfilesPath = Path.Combine(appPath, "Profiles");

        // Create a new profile and save it as a JSON file
        public override Profile Create(params object[] args)
        {
            Profile profile = new();

            // Save the new profile in its own directory
            CreateJsonFile(profile, Path.Combine(ProfilesPath, profile.Id.ToString()));
            return profile;
        }

        // Delete a profile directory and all its contents
        public void Delete(Profile profile)
        {
            string dirPath = Directory.GetDirectories(ProfilesPath, profile.Id, SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (dirPath != null)
            {
                Directory.Delete(dirPath, true); // Recursive delete
            }
        }

        // Add a new backup to a profile
        public void AddBackup(Profile profile, IEnumerable<string> contents)
        {
            string path = Path.Combine(ProfilesPath, profile.Id, "Backups");

            // Create a new backup and add its Id to the profile's Backups list
            profile.Backups.Add(_backupRepository.Create(path, contents).Id);

            // Update profile JSON to reflect the added backup
            Update(profile);
        }

        // Remove specific backups from a profile
        public void RemoveBackup(Profile profile, HashSet<string> backups)
        {
            // Remove backup Ids from profile's Backups list
            profile.Backups.RemoveAll(b => backups.Contains(b));

            // Update profile JSON after removal
            Update(profile);

            // Delete backup files corresponding to removed backups
            string backupsPath = Path.Combine(ProfilesPath, profile.Id.ToString(), "Backups");
            _backupRepository.Delete(backupsPath, backups);
        }
    }
}
