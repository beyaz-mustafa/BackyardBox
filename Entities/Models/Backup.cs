using Entities.Enums;

namespace Entities.Models
{
    // Represents a backup instance containing its data, destinations, and status
    public class Backup : Entity
    {
        public Backup()
        {
            Status = BackupStatus.None;
        }
        public List<string> Contents { get; set; } = new();
        public List<string> DestinationPaths { get; set; } = new();
        public List<string> Saves { get; set; } = new();
        public BackupStatus Status { get; set; }
        public FileSystemWatcher FileWatcher { get; init; } = new();
    }
}
