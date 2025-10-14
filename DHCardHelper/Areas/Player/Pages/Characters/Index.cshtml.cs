using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs.Character;
using DHCardHelper.Utilities.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DHCardHelper.Areas.Player.Pages.Characters
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;

        public List<CharacterSheetDto> CharacterSheets { get; set; } = new List<CharacterSheetDto>();

        public IndexModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId.IsNullOrEmpty())
            {
                TempData["Error"] = "User is not found. Please try to login again!";
                _logger.Error($"User is not found in Areas.Player.Pages.Characters - OnGetAsync");
                return Page();
            }

            var sheetsList = await _unitOfWork.CharacterSheetRepository.GetListWithFilterAsync(s => s.UserId == userId);
            CharacterSheets = sheetsList.Adapt<List<CharacterSheetDto>>();

            return Page();
        }
    }
}
