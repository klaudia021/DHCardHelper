using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Characters;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICardRepository CardRepository { get; }
        IRepository<Domain> DomainRepository { get; }
        IRepository<DomainCardType> DomainCardTypeRepository { get; }
        IRepository<CharacterClass> CharacterClassRepository { get; }
        IRepository<BackgroundCardType> BackgroundCardTypeRepository { get; }
        IRepository<CharacterSheet> CharacterSheetRepository { get; }
        IClassToDomainRel ClassToDomainRelRepository { get; }
        ICardSheetRepository CardSheetRepository { get; }

        Task<int> SaveAsync();
    }
}
