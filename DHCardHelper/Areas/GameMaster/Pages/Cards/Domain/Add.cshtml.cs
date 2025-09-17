using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using MapsterMapper;
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
        private readonly IMapper _mapper;
        public AddModel(IUnitOfWork unitOfWork, IMapper mapper, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [BindProperty]
        public UpsertDomainViewModel DomainViewModel { get; set; } = new UpsertDomainViewModel();

        public async Task OnGetAsync()
        {
            await PopulateDropDowns();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await PopulateDropDowns();

            if (!ModelState.IsValid)
                return Page();

            var domainForeignKeyValid = await this.IsForeignKeyValid(_unitOfWork.DomainRepository, d => d.Id == DomainViewModel.DomainCardDto.DomainId);
            if (!domainForeignKeyValid)
            {
                this.AddErrorToModel(() => DomainViewModel.DomainCardDto.DomainId);

                return Page();
            }

            var typeForeignKeyValid = await this.IsForeignKeyValid(_unitOfWork.DomainCardTypeRepository, t => t.Id == DomainViewModel.DomainCardDto.TypeId);
            if (!typeForeignKeyValid)
            {
                this.AddErrorToModel(() => DomainViewModel.DomainCardDto.TypeId);

                return Page();
            }

            DomainCard newEntity = _mapper.Map<DomainCard>(DomainViewModel.DomainCardDto);
            await _unitOfWork.CardRepository.AddAsync(newEntity);
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

            var availableTypes = await _unitOfWork.DomainCardTypeRepository.GetAllAsync();
            DomainViewModel.AvailableTypes = availableTypes.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            });
        }
    }
}

