using DHCardHelper.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using DHCardHelper.Models.Entities.Cards;

namespace DHCardHelper.Utilities.SeedDatabase
{
    public class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(IUnitOfWork unitOfWork)
        {
            List<string> domains = new List<string> { "air", "darkness", "earth", "fire", "light", "water" };
            await SeedDomainAsync<DomainCard>(unitOfWork, domains);
        }
        private static async Task SeedDomainAsync<T>(IUnitOfWork unitOfWork, List<string> files) where T : Card
        {
            foreach (var file in files)
                await AddToDbAsync<T>(unitOfWork, await ReadFromJsonAsync<T>(file));
        }
        private static async Task AddToDbAsync<T>(IUnitOfWork unitOfWork, List<T> cardList) where T : Card
        {
            foreach (var card in cardList)
            {
                try
                {
                    await unitOfWork.CardRepository.AddAsync(card);
                    await unitOfWork.SaveAsync();
                }
                catch (Exception ex) 
                { 
                    Debug.WriteLine($"Failed to insert item with ID {card.Id}: {ex.Message}");
                }
            }

        }
        private static async Task<List<T>> ReadFromJsonAsync<T>(string fileName) where T : Card
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Data", fileName.ToLower() + ".json");
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"File not found: {filePath}");

                var json = await File.ReadAllTextAsync(filePath);

                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when trying to read from file {fileName}.json: {ex.Message} ");
                return new List<T>();
            }
        }
    }
}
