using DHCardHelper.Auth;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.Types;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using DHCardHelper.Utilities;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Subclass
{
    [Area("GameMaster")]
    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.GameMaster}")]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        private readonly IMapper _mapper;

        public EditModel(IUnitOfWork unitOfWork, IMyLogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [BindProperty]
        public UpsertSubclassViewModel SubclassViewModel { get; set; } = new UpsertSubclassViewModel();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var entity = await _unitOfWork.CardRepository.GetFirstOrDefaultAsync<SubclassCard>(c => c.Id == id);
            if (entity == null)
                return NotFound();

            await PopulateDropDowns();

            SubclassViewModel.SubclassCardDto = _mapper.Map<SubclassCardDto>(entity);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await PopulateDropDowns();

            if (!ModelState.IsValid)
                return Page();

            var entity = await _unitOfWork.CardRepository.GetFirstOrDefaultAsync<SubclassCard>(c => c.Id == id);
            if (entity == null)
                return NotFound();

            var characterClassForeignKeyIsValid = await this.IsForeignKeyValid(_unitOfWork.CharacterClassRepository, c => c.Id == SubclassViewModel.SubclassCardDto.CharacterClassId);
            if (!characterClassForeignKeyIsValid)
            {
                this.AddErrorToModel(() => SubclassViewModel.SubclassCardDto.CharacterClassId);

                return Page();
            }

            _mapper.Map(SubclassViewModel.SubclassCardDto, entity);

            try
            {
                await _unitOfWork.SaveAsync();
                TempData["Success"] = "Subclass edited successfully";

                return Redirect("/Player/Cards/Subclasses/Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.Error(ex.Message);
                TempData["Error"] = "Unable to save data. Please check the data.";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                TempData["Error"] = "There was a database error. Please try again.";
            }

            return Page();
        }

        private async Task PopulateDropDowns()
        {
            var availableClasses = await _unitOfWork.CharacterClassRepository.GetAllAsync();
            SubclassViewModel.AvailableClasses = availableClasses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == SubclassViewModel.SubclassCardDto.CharacterClassId
            });

            SubclassViewModel.AvailableMasteryTypes =
                Enum.GetValues(typeof(MasteryType))
                    .Cast<MasteryType>()
                    .Select(m => new SelectListItem
                    {
                        Value = m.ToString(),
                        Text = m.ToString(),
                        Selected = m == SubclassViewModel.SubclassCardDto.MasteryType
                    });
        }
    }
}
