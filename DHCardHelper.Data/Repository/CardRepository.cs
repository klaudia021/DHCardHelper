using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<TDerived>> GetAllByTypeAsync<TDerived>() where TDerived : Card
        {
            return await _db.Set<Card>()
                .OfType<TDerived>()
                .ToListAsync();
        }
    }
}
