using DHCardHelper.Areas.GameMaster.Pages.Cards.Background;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Utilities.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace DHCardHelper.Tests.DHCardHelper.Areas.GameMaster.Pages.Cards.Background
{
    public class AddModelTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<BackgroundCardType>> _mockBackgroundCardTypeRepo;
        private readonly Mock<IMyLogger> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ICardRepository> _mockCardRepo;
        private readonly AddModel _addPageModel;

        public AddModelTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBackgroundCardTypeRepo = new Mock<IRepository<BackgroundCardType>>();
            _mockLogger = new Mock<IMyLogger>();
            _mockMapper = new Mock<IMapper>();
            _mockCardRepo = new Mock<ICardRepository>();

            _mockUnitOfWork.Setup(u => u.BackgroundCardTypeRepository)
                .Returns(_mockBackgroundCardTypeRepo.Object);

            _mockCardRepo.Setup(c => c.AddAsync(new BackgroundCard()));

            _mockUnitOfWork.Setup(u => u.CardRepository)
                .Returns(_mockCardRepo.Object);

            _addPageModel = new AddModel(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object)
            {
                BackgroundViewModel = new UpsertBackgroundViewModel()
            };
        }

        [Fact]
        public async Task OnGetAsync_BackgroundCardExists_PopulatesTheDropDown()
        {
            // Arrange
            _mockBackgroundCardTypeRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<BackgroundCardType>
                {
                    new BackgroundCardType { Id = 1, Name = "Background card 1" },
                    new BackgroundCardType { Id = 2, Name = "Background card 2" }
                });

            // Act
            await _addPageModel.OnGetAsync();

            // Assert
            var backgroundTypes = _addPageModel.BackgroundViewModel.HeritageTypes.ToList();

            Assert.Equal(2, backgroundTypes.Count);
            Assert.Equal("1", backgroundTypes[0].Value);
            Assert.Equal("Background card 1", backgroundTypes[0].Text);
            Assert.Equal("2", backgroundTypes[1].Value);
            Assert.Equal("Background card 2", backgroundTypes[1].Text);
        }


        [Fact]
        public async Task OnGetAsync_BackgroundCardDoesNotExist_DropDownWillBeEmpty()
        {
            // Arrange
            _mockBackgroundCardTypeRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<BackgroundCardType>());

            // Act
            await _addPageModel.OnGetAsync();

            // Assert
            var backgroundTypes = _addPageModel.BackgroundViewModel.HeritageTypes.ToList();

            Assert.Empty(backgroundTypes);
        }


        [Fact]
        public async Task OnPostAsync_ModelStateIsInvalid_ReturnsPage()
        {
            // Arrange
            _addPageModel.ModelState.AddModelError("Key", "Error");

            // Act
            var result = await _addPageModel.OnPostAsync();

            // Assert
            Assert.False(_addPageModel.ModelState.IsValid);
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_ForeignKeyIsNotValid_ReturnsPage()
        {
            // Arrange
            _mockBackgroundCardTypeRepo.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<BackgroundCardType, bool>>>()))
                    .ReturnsAsync(false);

            // Act
            var result = await _addPageModel.OnPostAsync();

            // Assert
            Assert.False(_addPageModel.ModelState.IsValid);
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_SavingWasSuccessful_RedirectToAddPage()
        {
            // Arrange
            _mockBackgroundCardTypeRepo.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<BackgroundCardType, bool>>>()))
                .ReturnsAsync(true);

            Mock<ITempDataDictionary> MockTempData = new Mock<ITempDataDictionary>();
            _addPageModel.TempData = MockTempData.Object;

            // Act
            var result = await _addPageModel.OnPostAsync();

            // Assert
            Assert.IsType<RedirectResult>(result);
            Assert.Equal("./Add", ((RedirectResult)result).Url);
        }

        [Theory]
        [InlineData(typeof(DbUpdateException))]
        [InlineData(typeof(Exception))]
        public async Task OnPostAsync_SavingWasNotSuccessfulAndThrowsException_LogsAndReturnsPage(Type exceptionType)
        {
            // Arrange
            _mockBackgroundCardTypeRepo.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<BackgroundCardType, bool>>>()))
                .ReturnsAsync(true);

            _mockUnitOfWork.Setup(u => u.SaveAsync())
                .Throws((Exception)Activator.CreateInstance(exceptionType));

            Mock<ITempDataDictionary> MockTempData = new Mock<ITempDataDictionary>();
            _addPageModel.TempData = MockTempData.Object;

            _mockLogger.Setup(l => l.Error("Log error"));

            // Act
            var result = await _addPageModel.OnPostAsync();

            // Assert
            _mockLogger.Verify(l => l.Error(It.IsAny<string>()), Times.Once);
            Assert.IsType<PageResult>(result);
        }
    }
}
