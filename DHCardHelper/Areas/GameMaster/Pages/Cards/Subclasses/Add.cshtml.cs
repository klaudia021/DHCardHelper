using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.Types;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Subclass
{
    [Area("GameMaster")]
    public class AddModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        private readonly IMapper _mapper;

        public AddModel(IUnitOfWork unitOfWork, IMyLogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [BindProperty]
        public UpsertSubclassViewModel SubclassViewModel { get; set; } = new UpsertSubclassViewModel();

        public async Task OnGetAsync()
        {
            await PopulateDropDowns();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await PopulateDropDowns();

            if (!ModelState.IsValid)
                return Page();

            var characterClassForeignKeyIsValid = await this.IsForeignKeyValid(_unitOfWork.CharacterClassRepository, c => c.Id == SubclassViewModel.SubclassCardDto.CharacterClassId);
            if (!characterClassForeignKeyIsValid)
            {
                this.AddErrorToModel(() => SubclassViewModel.SubclassCardDto.CharacterClassId);

                return Page();
            }

            SubclassCard newEntity = _mapper.Map<SubclassCard>(SubclassViewModel.SubclassCardDto);
            await _unitOfWork.CardRepository.AddAsync(newEntity);

            try
            {
                await _unitOfWork.SaveAsync();
                TempData["Success"] = "Subclass added successfully";

                return Redirect("./Add");
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
                Text = c.Name
            });

            SubclassViewModel.AvailableMasteryTypes = 
                Enum.GetValues(typeof(MasteryType))
                    .Cast<MasteryType>()
                    .Select(m => new SelectListItem
                    {
                        Value = m.ToString(),
                        Text = m.ToString()
                    });
        }
    }
}
