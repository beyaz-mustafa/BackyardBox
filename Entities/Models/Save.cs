namespace Entities.Models
{
    public class Save
    {
        public Save(string path, Backup backup)
        {
            Path = path;
            Backup = backup;
            //Id = UniqueIdGeneratorMethod();
        }
        public int Id { get; set; }
        public string Path { get; set; }
        public Backup Backup { get; set; }
    }
}
