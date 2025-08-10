using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Domain
{
    [Area("GameMaster")]
    public class EditModel : PageModel
    {
        private readonly IMyLogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public UpsertDomainViewModel DomainViewModel { get; set; } = new UpsertDomainViewModel();
        public EditModel(IMyLogger myLogger, IUnitOfWork unitOfWork)
        {
            _logger = myLogger;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var domain = await _unitOfWork.CardRepository.GetFirstOrDefaultAsync<DomainCard>(d => d.Id == id);
            if (domain == null)
                return NotFound();

            DomainViewModel.DomainCard = domain;
            await PopulateDropdown();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
                return Page();

            if (id == null)
                return NotFound();

            var entity = await _unitOfWork.CardRepository
                .GetFirstOrDefaultAsync<DomainCard>(d => d.Id == id);
            if (entity == null)
                return NotFound();


            return Page();
        }


        private async Task PopulateDropdown()
        {
            var domains = await _unitOfWork.DomainRepository.GetAllAsync();
            var types = await _unitOfWork.TypeRepository.GetAllAsync();

            DomainViewModel.AvailableDomains = domains.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString(),
                Selected = ShouldBeSelected(d.Id)
            });

            DomainViewModel.AvailableTypes = types.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString(),
                Selected = ShouldBeSelected(t.Id)
            });
        }

        private bool ShouldBeSelected(int id)
        {
            if (DomainViewModel.DomainCard == null)
                return false;

            if (DomainViewModel.DomainCard.Id == id)
                return true;

            return false;
        }

    }
}
