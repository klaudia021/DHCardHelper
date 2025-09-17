using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHCardHelper.Pages.Cards.Subclasses
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<SubclassCard> SubclassCards { get; set; } = new List<SubclassCard>();

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;

        public IndexModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            var subclassCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<SubclassCard>(s => s.CharacterClass);

            SubclassCards = subclassCards;

        }
    }
}
