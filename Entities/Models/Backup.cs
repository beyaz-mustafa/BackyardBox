using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Backup
    {
        public Backup(string sourcePath)
        {
            //Id = UniqueIdGeneratorMethod();
            //Name = UniqueNameGeneratorMethod();
            SourcePath = sourcePath;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Alias { get; set; }
        public string SourcePath { get; set; }
        public List<string> DestinationPaths { get; set; } = new List<string>();
        public int MaxSaves { get; set; }
        public List<Save> Saves { get; set; } = new List<Save>();
        public BackupStatus Status { get; set; }
    }
}
