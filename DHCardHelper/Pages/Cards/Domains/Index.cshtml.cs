using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DHCardHelper.Models;
using System.Text.Json;
using DHCardHelper.Services;
using DHCardHelper.Data.Repository.IRepository;
using DomainModel = DHCardHelper.Models.Cards.DomainCard;
using DHCardHelper.Models.Cards;

namespace DHCardHelper.Pages.Cards.Domains
{
    public class IndexModel : PageModel
    {
        public IEnumerable<DomainModel> _domainCards { get; set; } = new List<DomainModel>();

        public IEnumerable<DomainModel> _filteredCards { get; set; } = new List<DomainModel>();
        //public List<ClassDomainRelationDto> _classDomainRelations { get; set; } = new List<ClassDomainRelationDto>();
        public IEnumerable<string> _availableDomains { get; set; }

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
            await LoadAllDataAsync();
        }

        private async Task LoadAllDataAsync()
        {
            _domainCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<DomainModel>();
            _filteredCards = _domainCards;

            _availableDomains = _domainCards
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
            //    _filteredCards = _filteredCards.Where(c => (c.Domain == selectedClass.Domains[0] || c.Domain == selectedClass.Domains[1])).ToList();
            //}
        }

        private async Task FilterDomain()
        {
            var domainCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<DomainModel>();

            if (SelectedDomain == "All")
                _filteredCards = domainCards;
            else
                _filteredCards = domainCards.Where(c => c.Domain == SelectedDomain);
        }
    }
}
