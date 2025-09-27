using DHCardHelper.Models.DTOs.Character;
using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Relationships;
using DHCardHelper.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface IClassToDomainRel : IRepository<ClassToDomainRel>
    {
        public Task<List<CharacterClassDto>> GetDomainsForClass();
    }
}
