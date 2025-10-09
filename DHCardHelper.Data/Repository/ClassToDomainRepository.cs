using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs.Character;
using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Relationships;
using Microsoft.EntityFrameworkCore;

namespace DHCardHelper.Data.Repository
{
    public class ClassToDomainRepository : Repository<ClassToDomainRel> ,IClassToDomainRel
    {
        private readonly ApplicationDbContext _db;
        public ClassToDomainRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<List<CharacterClassDto>> GetDomainsForClass()
        {
            return await _db.CharacterClasses
                .Include(c => c.ClassToDomainRel)
                .ThenInclude(cd => cd.Domain)
                .Select(c => new CharacterClassDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Domains = c.ClassToDomainRel.Select(cd => new Domain
                    {
                        Id = cd.DomainId,
                        Name = cd.Domain.Name,
                        Color = cd.Domain.Color
                    }).ToList()
                }).ToListAsync();
        }

    }
}
