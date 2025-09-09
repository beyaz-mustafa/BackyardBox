namespace Entities.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}
