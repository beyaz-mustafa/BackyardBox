using Entities.Models;

namespace Repositories.RepositoryModels
{
    // Repository class for managing Save entities
    public class SaveRepository : BaseRepository<Save>
    {
        // Create a new save and store it as a JSON file
        public override Save Create(params object[] args)
        {
            Save save = new();

            // First argument is the path where the save folder will be created
            string path = Path.Combine((string)args[0], save.Id.ToString());

            // Save the Save entity as a JSON file in its dedicated folder
            CreateJsonFile(save, path);

            return save;
        }
    }
}
