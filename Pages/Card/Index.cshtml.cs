using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DHCardHelper.Models;
using System.Text.Json;
using DHCardHelper.Services;

namespace DHCardHelper.Pages.Card
{
    public class IndexModel : PageModel
    {
        public List<CardDto> Cards { get; set; } = new List<CardDto>();
        public List<CardDto> FilteredCards { get; set; } = new List<CardDto>();
        public List<ClassDomainRelationDto> _classDomainRelations { get; set; } = new List<ClassDomainRelationDto>();
        public string[] _availableDomains { get; set; }

        [BindProperty]
        public string SelectedDomain { get; set; }
        [BindProperty]
        public string SelectedClass { get; set; }

        private readonly IMyLogger _logger;
        private readonly IWebHostEnvironment _env;
        public IndexModel(IWebHostEnvironment env, IMyLogger logger)
        {
            _env = env;
            _logger = logger;
        }
        public async Task OnGet()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            _availableDomains = await ReadAvailableDomains();

            if (_availableDomains.Length == 0)
                throw new ArgumentNullException("There are no available domains.");

            var IdCounter = 0;
            foreach (var domain in _availableDomains)
            {
                var domainCards = await ReadCardsFromJsonAsync($"{domain}.json");
                foreach (var card in domainCards)
                {
                    card.Id = IdCounter;
                    Cards.Add(card);
                    FilteredCards.Add(card);
                    IdCounter++;
                }
            }

            _classDomainRelations = await ReadClassesAndDomainsFromJsonAsync();
            _logger.Info($"Class:{_classDomainRelations[0].Class} \n Domains: {_classDomainRelations[0].Domains[0]} {_classDomainRelations[0].Domains[1]}");
        }
        public async Task<IActionResult> OnPostFilterApplied(string SelectedClass, string SelectedDomain)
        {
            await LoadData();

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
            var selectedClass = _classDomainRelations.Find(c => c.Class == SelectedClass);
            if (selectedClass != null)
            {
                FilteredCards = FilteredCards.Where(c => (c.Domain == selectedClass.Domains[0] || c.Domain == selectedClass.Domains[1])).ToList();
            }
        }

        private void FilterDomain()
        {
            if (SelectedDomain == "All")
                FilteredCards = Cards;
            else
                FilteredCards = FilteredCards.Where(c => c.Domain == SelectedDomain).ToList();
        }
        private async Task<string[]> ReadAvailableDomains()
        {
            try
            {
                var filePath = Path.Combine(_env.ContentRootPath, "Data", "AvailableDomains.txt");
                var result = await System.IO.File.ReadAllLinesAsync(filePath);

                _logger.Info("File loaded successfully!");

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<CardDto>> ReadCardsFromJsonAsync(string name)
        {
            try
            {
                var filePath = Path.Combine(_env.ContentRootPath, "Data", name.ToLower());
                using var stream = System.IO.File.OpenRead(filePath);

                return await JsonSerializer.DeserializeAsync<List<CardDto>>(stream);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<ClassDomainRelationDto>> ReadClassesAndDomainsFromJsonAsync()
        {
            try
            {
                var filePath = Path.Combine(_env.ContentRootPath, "Data", "ClassesAndDomains.json");
                using var stream = System.IO.File.OpenRead(filePath);

                return await JsonSerializer.DeserializeAsync<List<ClassDomainRelationDto>>(stream);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
