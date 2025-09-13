namespace Entities.Models
{
    // Base abstract class for entities, provides a unique identifier
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}
