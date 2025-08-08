using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Cards;
using DHCardHelper.Models.Entities.AvailableTypes;

namespace DHCardHelper.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        private ICardRepository? _cardRepository;
        private IRepository<Domain>? _domainRepository;
        private IRepository<Models.Entities.AvailableTypes.Type>? _typeRepository;
        public UnitOfWork(ApplicationDbContext db) => _db = db;

        public ICardRepository CardRepository => _cardRepository ??= new CardRepository(_db);
        public IRepository<Domain> AvailableDomainRepository => _domainRepository ??= new Repository<Domain>(_db);
        public IRepository<Models.Entities.AvailableTypes.Type> AvailableTypeRepository => _typeRepository ??= new Repository<Models.Entities.AvailableTypes.Type>(_db);


        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
