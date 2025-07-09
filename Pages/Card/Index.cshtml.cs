using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DHCardHelper.Models;
using System.Text.Json;

namespace DHCardHelper.Pages.Card
{
    public class IndexModel : PageModel
    {
        public List<CardDto> Cards { get; set; } = new List<CardDto>();
        private readonly IWebHostEnvironment _env;
        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task OnGet()
        {
            //Cards = new List<CardDto>
            //{
            //    new CardDto{Domain = "Arcana", Feature="Some text here", Id = 1, Level = 1, RecallCost = 1, Title = "Arcana Card 1", Type = "Spell" },
            //    new CardDto{Domain = "Arcana", Feature="Some text here 2", Id = 2, Level = 1, RecallCost = 1, Title = "Arcana Card 2", Type = "Ability" },
            //    new CardDto{Domain = "Bone", Feature="Some text here 3", Id = 3, Level = 1, RecallCost = 1, Title = "Bone Card 1", Type = "Ability" },
            //};
            var arcanaCards = await ReadCardsFromJsonAsync("arcana.json");
            var bladeCards = await ReadCardsFromJsonAsync("blade.json");

            var IdCounter = 0;

            foreach (var card in arcanaCards)
            {
                card.Id = IdCounter;
                Cards.Add(card);
                IdCounter++;
            }

            foreach (var card in bladeCards)
            {
                card.Id = IdCounter;
                Cards.Add(card);
                IdCounter++;
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
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
