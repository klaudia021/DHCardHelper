using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Domain
{
    [Area("GameMaster")]
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        public AddModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [BindProperty]
        public UpsertDomainViewModel DomainViewModel { get; set; } = new UpsertDomainViewModel();

        public async Task OnGet()
        {
            await PopulateDropDowns();
        }
        public async Task<IActionResult> OnPost()
        {
            await PopulateDropDowns();

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The submitted data is not valid!";
                return Page();
            }

            await _unitOfWork.CardRepository.AddAsync(DomainViewModel.DomainCard);
            await _unitOfWork.SaveAsync();

            TempData["Success"] = "Domain card added successfully!";

            return Redirect("./Add");

        }
        private async Task PopulateDropDowns()
        {
            var availableDomains = await _unitOfWork.DomainRepository.GetAllAsync();
            DomainViewModel.AvailableDomains = availableDomains.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            });

            var availableTypes = await _unitOfWork.TypeRepository.GetAllAsync();
            DomainViewModel.AvailableTypes = availableTypes.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            });
        }
    }
}

