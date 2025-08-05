using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DHCardHelper.Models;
using System.Text.Json;
using DHCardHelper.Services;
using DHCardHelper.Data.Repository.IRepository;
using DomainModel = DHCardHelper.Models.Cards.DomainCard;
using DHCardHelper.Models.Cards;
using DHCardHelper.Utilities.SeedDatabase;
using Microsoft.IdentityModel.Tokens;

namespace DHCardHelper.Pages.Cards.Domains
{
    public class IndexModel : PageModel
    {
        public IEnumerable<DomainModel> DomainCards { get; set; } = new List<DomainModel>();

        public IEnumerable<DomainModel> FilteredCards { get; set; } = new List<DomainModel>();
        public IEnumerable<string> AvailableDomains { get; set; }

        [BindProperty]
        public string SelectedDomain { get; set; }
        [BindProperty]
        public string SelectedClass { get; set; }

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
                var allCards = await _unitOfWork.CardRepository.GetAllAsync();

                if (allCards.IsNullOrEmpty())
                {
                    await DatabaseSeeder.SeedDatabaseAsync(_unitOfWork);
                }
                
                await LoadAllDataAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        private async Task LoadAllDataAsync()
        {
            DomainCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<DomainModel>();
            FilteredCards = DomainCards;

            AvailableDomains = DomainCards
                .Select(c => c.Domain)
                .Distinct();
        }
        public async Task<IActionResult> OnPostFilterApplied(string SelectedClass, string SelectedDomain)
        {
            await LoadAllDataAsync();

            _logger.Info($"SelectedClass: {SelectedClass}\n SelectedDomain: {SelectedDomain}");
            if (SelectedClass != "All")
            {
                FilterClass();
                return Page();
            }

            if (SelectedDomain != "All")
            {
                FilterDomain();
                return Page();
            }

            return Page();
        }

        private void FilterClass()
        {
            //var selectedClass = _classDomainRelations.Find(c => c.Class == SelectedClass);
            //if (selectedClass != null)
            //{
            //    FilteredCards = FilteredCards.Where(c => (c.Domain == selectedClass.Domains[0] || c.Domain == selectedClass.Domains[1])).ToList();
            //}
        }

        private async Task FilterDomain()
        {
            var domainCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<DomainModel>();

            if (SelectedDomain == "All")
                FilteredCards = domainCards;
            else
                FilteredCards = domainCards.Where(c => c.Domain == SelectedDomain);
        }
    }
}
