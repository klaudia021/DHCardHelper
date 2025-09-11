using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DHCardHelper.Models;
using System.Text.Json;
using DHCardHelper.Services;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Utilities.SeedDatabase;
using Microsoft.IdentityModel.Tokens;

namespace DHCardHelper.Pages.Cards.Domains
{
    public class IndexModel : PageModel
    {
        public IEnumerable<DomainCard> DomainCards { get; set; } = new List<DomainCard>();

        private readonly IMyLogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IMyLogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task OnGet()
        {
            try
            {
                var allDomainCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<DomainCard>(c => c.DomainCardType, c=> c.Domain);

                if (allDomainCards.IsNullOrEmpty())
                {
                    await DatabaseSeeder.SeedDatabaseAsync(_unitOfWork);
                }

                DomainCards = allDomainCards;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
