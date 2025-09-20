using DHCardHelper.Auth;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Domain
{
    [Area("GameMaster")]
    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.GameMaster}")]
    public class EditModel : PageModel
    {
        private readonly IMyLogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        [BindProperty]
        public UpsertDomainViewModel DomainViewModel { get; set; } = new UpsertDomainViewModel();
        public EditModel(IMyLogger myLogger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = myLogger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var entity = await _unitOfWork.CardRepository.GetFirstOrDefaultAsync<DomainCard>(d => d.Id == id);
            if (entity == null)
                return NotFound();

            DomainViewModel.DomainCardDto = _mapper.Map<DomainCardDto>(entity);

            await PopulateDropdowns();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await PopulateDropdowns();

            if (!ModelState.IsValid)
                return Page();

            var entity = await _unitOfWork.CardRepository
                .GetFirstOrDefaultAsync<DomainCard>(d => d.Id == id);
            if (entity == null)
                return NotFound();

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

            _mapper.Map(DomainViewModel.DomainCardDto, entity);
            await _unitOfWork.SaveAsync();

            TempData["Success"] = "Domain card edited successfully!";

            return Redirect("/Player/Cards/Domains/Index");
        }

        private async Task PopulateDropdowns()
        {
            var domains = await _unitOfWork.DomainRepository.GetAllAsync();
            var types = await _unitOfWork.DomainCardTypeRepository.GetAllAsync();

            DomainViewModel.AvailableDomains = domains.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString(),
                Selected = d.Id == DomainViewModel.DomainCardDto.DomainId
            });

            DomainViewModel.AvailableTypes = types.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString(),
                Selected = t.Id == DomainViewModel.DomainCardDto.TypeId
            });
        }
    }
}
