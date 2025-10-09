using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs.Character;
using DHCardHelper.Models.Entities.Characters;
using DHCardHelper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DHCardHelper.Areas.Player.Pages.Characters
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;

        [BindProperty]
        public CharacterSheetDto CharacterSheetDto { get; set; }
        public AddModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId.IsNullOrEmpty())
            {
                TempData["Error"] = "User is not found. Please try to login again!";
                return Page();
            }

            var entity = new CharacterSheet
            {
                Name = CharacterSheetDto.Name,
                UserId = userId
            };

            await _unitOfWork.CharacterSheetRepository.AddAsync(entity);

            try
            {
                await _unitOfWork.SaveAsync();
                TempData["Success"] = "Character created successfully!";

                return Redirect($"/Player/Characters/Index");
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = "Unable to save the data. Please check the data!";
                _logger.Error(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was a database error. Please try again!";
                _logger.Error(ex.Message);
            }

            return Page();
        }
    }
}
