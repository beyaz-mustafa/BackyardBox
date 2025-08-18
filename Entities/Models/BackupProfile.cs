using Entities.Enums;

namespace Entities.Models
{
    public class BackupProfile
    {
        public BackupProfile()
        {
            //Id = UniqueIdGeneratorMethod();
            Status = BackupStatus.None;
        }

        public int Id { get; set; }
        public string? Alias { get; set; }
        public int MaxSaves { get; set; }
        public List<Backup> Backups { get; set; } = new List<Backup>();
        public BackupStatus Status { get; set; }
    }
}
