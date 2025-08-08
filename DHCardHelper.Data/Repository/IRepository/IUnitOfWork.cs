using DHCardHelper.Models.Entities;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICardRepository CardRepository { get; }
        IRepository<Domain> DomainRepository { get; }
        IRepository<Models.Entities.Type> TypeRepository { get; }

        Task<int> SaveAsync();
    }
}
