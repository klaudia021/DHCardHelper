using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities;

namespace DHCardHelper.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        private ICardRepository? _cardRepository;
        private IRepository<Domain>? _domainRepository;
        private IRepository<DomainCardType>? _domainCardTypeRepository;
        private IRepository<CharacterClass>? _characterClassRepository;
        private IRepository<BackgroundCardType>? _backgroundCardTypeRepository;
        public UnitOfWork(ApplicationDbContext db) => _db = db;

        public ICardRepository CardRepository => _cardRepository ??= new CardRepository(_db);
        public IRepository<Domain> DomainRepository => _domainRepository ??= new Repository<Domain>(_db);
        public IRepository<DomainCardType> DomainCardTypeRepository => _domainCardTypeRepository ??= new Repository<DomainCardType>(_db);
        public IRepository<CharacterClass> CharacterClassRepository => _characterClassRepository ??= new Repository<CharacterClass>(_db);
        public IRepository<BackgroundCardType> BackgroundCardTypeRepository => _backgroundCardTypeRepository ??= new Repository<BackgroundCardType>(_db);


        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
