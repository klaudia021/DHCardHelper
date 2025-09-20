using DHCardHelper.Auth;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Background
{
    [ValidateAntiForgeryToken]
    [Area("GameMaster")]
    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.GameMaster}")]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        public DeleteModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        public async Task<IActionResult> OnDeleteAsync(int? id)
        {
            var entity = await _unitOfWork.CardRepository.GetFirstOrDefaultAsync<BackgroundCard>(c => c.Id == id);
            if (entity == null)
            {
                return new JsonResult(new { success = false, message = "Item is not found!" });
            }

            _unitOfWork.CardRepository.Remove(entity);

            try
            {
                await _unitOfWork.SaveAsync();

                return new JsonResult(new { success = true, message = "Item deleted successfully!" });
            }
            catch (DbUpdateException ex)
            {
                _logger.Error(ex.Message);

                return new JsonResult(new { success = false, message = "Unable to save data. Please check the data." });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return new JsonResult(new { success = false, message = "There was a database error. Please try again." });
            }
        }
    }
}
