namespace Brasserie.IJK.Api.Domain.Common
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        protected Entity()
        {

        }

        protected Entity(int id)
        {
            Id = id;
        }
    }
}
