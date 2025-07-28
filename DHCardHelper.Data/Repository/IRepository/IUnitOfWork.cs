using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHCardHelper.Models.Cards;
using DHCardHelper.Models.Domains;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICardRepository CardRepository { get; }
        IRepository<AvailableDomain> DomainRepository { get; }

        Task<int> SaveAsync();
    }
}
