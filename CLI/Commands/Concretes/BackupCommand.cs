using CLI.Commands.Abstracts;
using Entities.Models;

namespace CLI.Commands.Concretes
{
    public class BackupCommand : IBackupCommand
    {
        public Backup Find(int id)
        {
            // This method should implement logic to find a backup by its ID.
            // For now, we will throw an exception to indicate that this method is not implemented.
            throw new NotImplementedException("Find method is not implemented.");
        }
    }
}
