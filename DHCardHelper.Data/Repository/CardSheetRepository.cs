using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.Entities.Characters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DHCardHelper.Data.Repository
{
    public class CardSheetRepository : Repository<CardSheet>, ICardSheetRepository
    {
        private readonly ApplicationDbContext _db;
        public CardSheetRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CardSheet>> GetAllCharacterCards(int? characterId)
        {
            if (characterId == null)
                return new List<CardSheet>();

            return await _db.CardSheet
                .Where(s => s.CharacterSheetId == characterId)
                .Include(c => c.Card)
                .Include(c => c.Card.Domain)
                .Include(c => c.Card.DomainCardType)
                .Include(c => c.Card.CharacterClass)
                .Include(c => c.Card.BackgroundType)
                .Include(s => s.CharacterSheet)
                .OrderBy(c => EF.Property<string>(c.Card, "CardType"))
                .ToListAsync();
        }
    }
}
