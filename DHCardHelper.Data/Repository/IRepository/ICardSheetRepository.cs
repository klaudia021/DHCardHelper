using DHCardHelper.Models.Entities.Characters;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface ICardSheetRepository : IRepository<CardSheet>
    {
        Task<IEnumerable<CardSheet>> GetAllCharacterCards(int? characterId);
    }
}
