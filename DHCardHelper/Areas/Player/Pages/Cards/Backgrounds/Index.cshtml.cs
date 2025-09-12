using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHCardHelper.Pages.Cards.Backgrounds
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<BackgroundCard> BackgroundCards { get; set; } = new List<BackgroundCard>();

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;

        public IndexModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            var backgroundCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<BackgroundCard>(b => b.BackgroundType);

            BackgroundCards = backgroundCards;

        }
    }
}
