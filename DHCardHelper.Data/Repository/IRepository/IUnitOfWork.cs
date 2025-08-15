using DHCardHelper.Models.Entities;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICardRepository CardRepository { get; }
        IRepository<Domain> DomainRepository { get; }
        IRepository<DomainCardType> DomainCardTypeRepository { get; }
        IRepository<CharacterClass> CharacterClassRepository { get; }
        IRepository<BackgroundCardType> BackgroundCardTypeRepository { get; }

        Task<int> SaveAsync();
    }
}
