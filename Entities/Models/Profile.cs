using Entities.Enums;

namespace Entities.Models
{
    // Represents a backup profile that contains information about backups
    public class Profile : Entity
    {
        public Profile()
        {
            Status = BackupStatus.None;
        }
        public string? Alias { get; set; }
        public int MaxSaves { get; set; }
        public List<string> Backups { get; set; } = new();
        public BackupStatus Status { get; set; }
    }
}
