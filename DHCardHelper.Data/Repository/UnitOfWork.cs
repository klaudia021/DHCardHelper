using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Cards;
using DHCardHelper.Models.Domains;

namespace DHCardHelper.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private ICardRepository? _cards { get; set; }
        private IRepository<AvailableDomain>? _domains { get; set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }
        public IRepository<AvailableDomain> DomainRepository => _domains ??= new Repository<AvailableDomain>(_db);
        public ICardRepository CardRepository => _cards ??= new CardRepository(_db);

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
