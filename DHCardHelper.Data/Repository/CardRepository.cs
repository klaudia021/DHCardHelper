using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DHCardHelper.Data.Repository
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        private readonly ApplicationDbContext _db;
        public CardRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TDerived>> GetAllByTypeAsync<TDerived>(params Expression<Func<TDerived, object>>[] includes) 
            where TDerived : Card
        {
            IQueryable<TDerived> query = _db.Set<TDerived>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
        public async Task<TDerived?> GetFirstOrDefaultAsync<TDerived>(
            Expression<Func<TDerived, bool>> filter) 
            where TDerived : Card
        {
            return await _db.Set<Card>()
                .OfType<TDerived>().FirstOrDefaultAsync(filter);
        }
    }
}
