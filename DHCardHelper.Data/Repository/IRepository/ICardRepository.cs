using System.Linq.Expressions;
using DHCardHelper.Models.Entities.Cards;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<IEnumerable<TDerived>> GetAllByTypeAsync<TDerived>()
            where TDerived : Card;
        Task<TDerived?> GetFirstOrDefaultAsync<TDerived>(Expression<Func<TDerived, bool>> filter)
            where TDerived : Card;
    }
}
