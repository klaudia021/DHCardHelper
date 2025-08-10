using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities;

namespace DHCardHelper.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        private ICardRepository? _cardRepository;
        private IRepository<Domain>? _domainRepository;
        private IRepository<Models.Entities.Type>? _typeRepository;
        public UnitOfWork(ApplicationDbContext db) => _db = db;

        public ICardRepository CardRepository => _cardRepository ??= new CardRepository(_db);
        public IRepository<Domain> DomainRepository => _domainRepository ??= new Repository<Domain>(_db);
        public IRepository<Models.Entities.Type> TypeRepository => _typeRepository ??= new Repository<Models.Entities.Type>(_db);


        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
