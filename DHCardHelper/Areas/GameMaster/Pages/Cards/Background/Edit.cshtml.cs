using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Background
{
    [Area("GameMaster")]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        private readonly IMapper _mapper;

        [BindProperty]
        public UpsertBackgroundViewModel BackgroundViewModel { get; set; } = new UpsertBackgroundViewModel();

        public EditModel(IUnitOfWork unitOfWork, IMyLogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var entity = await _unitOfWork.CardRepository.GetFirstOrDefaultAsync<BackgroundCard>(c => c.Id == id);
            if (entity == null)
                return NotFound();

            await PopulateDropDown();

            BackgroundViewModel.BackgroundCardDto = _mapper.Map<BackgroundCardDto>(entity);

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await PopulateDropDown();

            if (!ModelState.IsValid)
                return Page();

            var entity = await _unitOfWork.CardRepository.GetFirstOrDefaultAsync<BackgroundCard>(c => c.Id == id);
            if (entity == null)
                return NotFound();

            var backgroundTypeForeignKeyValid = await this.IsForeignKeyValid(_unitOfWork.BackgroundCardTypeRepository, t => t.Id == BackgroundViewModel.BackgroundCardDto.BackgroundTypeId);
            if (!backgroundTypeForeignKeyValid)
            {
                this.AddErrorToModel(() => BackgroundViewModel.BackgroundCardDto.BackgroundTypeId);

                return Page();
            }

            _mapper.Map(BackgroundViewModel.BackgroundCardDto, entity);
            await _unitOfWork.SaveAsync();

            TempData["Success"] = "Heritage card edited successfully!";

            return Redirect("/Player/Cards/Backgrounds/Index");
        }

        private async Task PopulateDropDown()
        {
            var backgroundTypes = await _unitOfWork.BackgroundCardTypeRepository.GetAllAsync();

            BackgroundViewModel.HeritageTypes = backgroundTypes.Select(t => new SelectListItem 
            { 
                Value = t.Id.ToString(),
                Text = t.Name,
                Selected = t.Id == BackgroundViewModel.BackgroundCardDto.BackgroundTypeId
            });
        } 
    }
}
