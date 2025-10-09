using DHCardHelper.Auth;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using DHCardHelper.Utilities;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Background
{
    [Area("GameMaster")]
    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.GameMaster}")]
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        private readonly IMapper _mapper;

        [BindProperty]
        public UpsertBackgroundViewModel BackgroundViewModel { get; set; } = new UpsertBackgroundViewModel();

        public AddModel(IUnitOfWork unitOfWork, IMyLogger mylogger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = mylogger;
            _mapper = mapper;
        }
        public async Task OnGetAsync()
        {
            await PopulateDropDown();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await PopulateDropDown();

            if (!ModelState.IsValid)
                return Page();

            var backgroundTypeForeignKeyValid = await this.IsForeignKeyValid(_unitOfWork.BackgroundCardTypeRepository, t => t.Id == BackgroundViewModel.BackgroundCardDto.BackgroundTypeId);
            if (!backgroundTypeForeignKeyValid)
            {
                this.AddErrorToModel(() => BackgroundViewModel.BackgroundCardDto.BackgroundTypeId);

                return Page();
            }

            BackgroundCard newEntity = _mapper.Map<BackgroundCard>(BackgroundViewModel.BackgroundCardDto);
            await _unitOfWork.CardRepository.AddAsync(newEntity);

            try
            {
                await _unitOfWork.SaveAsync();
                TempData["Success"] = "Heritage added successfully";

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

        private async Task PopulateDropDown()
        {
            var heritageTypes = await _unitOfWork.BackgroundCardTypeRepository.GetAllAsync();

            BackgroundViewModel.HeritageTypes = heritageTypes.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.Name
            });
        }
    }
}
