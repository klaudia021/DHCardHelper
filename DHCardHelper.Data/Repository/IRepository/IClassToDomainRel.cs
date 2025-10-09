using DHCardHelper.Models.DTOs.Character;
using DHCardHelper.Models.Entities.Relationships;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface IClassToDomainRel : IRepository<ClassToDomainRel>
    {
        public Task<List<CharacterClassDto>> GetDomainsForClass();
    }
}
