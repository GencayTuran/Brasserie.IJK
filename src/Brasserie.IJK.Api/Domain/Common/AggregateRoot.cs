namespace Brasserie.IJK.Api.Domain.Common
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot()
        {
        }

        protected AggregateRoot(Guid id) 
            : base(id)
        {
        }
    }
}
