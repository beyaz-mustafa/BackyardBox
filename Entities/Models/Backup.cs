using Entities.Enums;

namespace Entities.Models
{
    public class Backup
    {
        public Backup(string sourcePath, BackupProfile profile)
        {
            //Id = UniqueIdGeneratorMethod();
            //Name = UniqueNameGeneratorMethod();
            SourcePath = sourcePath;
            Status = BackupStatus.None;
            Profile = profile;
        }
        public int Id { get; set; }
        public string SourcePath { get; set; }
        public List<string> DestinationPaths { get; set; } = new List<string>();
        public List<Save> Saves { get; set; } = new List<Save>();
        public BackupStatus Status { get; set; }
        public BackupProfile Profile { get; set; }
    }
}
