using Entities.Models;
using Repositories.RepositoryModels;

namespace Repositories.Concretes
{
    public class SaveRepository : BaseRepository<Save>
    {
        public override Save Create(params object[] args)
        {
            Save save = new();
            string path = Path.Combine((string)args[0], save.Id.ToString());

            CreateJsonFile(save, path);

            return save;
        }
    }
}
