using Entities.Models;
using Repositories.Abstracts;

namespace Repositories.Concretes
{
    public class SaveCommand : ISaveCommand
    {
        public Save Find(int id)
        {
            // This method should implement logic to find a Save by its ID.
            // For now, we will throw an exception to indicate that this method is not implemented.
            throw new NotImplementedException("Find method is not implemented.");
        }
    }
}
