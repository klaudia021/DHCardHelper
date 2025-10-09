using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs.Character;
using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHCardHelper.Areas.GameMaster.Pages.Characters
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public List<CharacterSheetDto> CharacterSheets { get; set; } = new List<CharacterSheetDto>();
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task OnGet()
        {
            var sheets = await _unitOfWork.CharacterSheetRepository.GetAllAsync();

            CharacterSheets = sheets.Adapt<List<CharacterSheetDto>>();
        }
    }
}
