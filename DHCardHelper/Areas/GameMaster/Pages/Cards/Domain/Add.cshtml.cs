using DHCardHelper.Data.Repository;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Domains;
using DHCardHelper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHCardHelper.Areas.GameMaster.Pages.Cards.Domain
{
    [Area("GameMaster")]
    public class AddModel : PageModel
    {
        public IEnumerable<AvailableDomain> _availableDomains { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        public AddModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task OnGet()
        {
            _availableDomains = await _unitOfWork.DomainRepository.GetAllAsync();
        }
    }
}
