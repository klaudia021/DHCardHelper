using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHCardHelper.Models.Cards;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<IEnumerable<TDerived>> GetAllByTypeAsync<TDerived>() where TDerived : Card;
    }
}
