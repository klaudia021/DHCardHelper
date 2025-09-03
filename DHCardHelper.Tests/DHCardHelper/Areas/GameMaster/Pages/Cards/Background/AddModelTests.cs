using Azure;
using DHCardHelper.Areas.GameMaster.Pages.Cards.Background;
using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System.Linq.Expressions;

namespace DHCardHelper.Tests.DHCardHelper.Areas.GameMaster.Pages.Cards.Background
{
    public class AddModelTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<BackgroundCardType>> _mockRepo;
        private readonly Mock<IMyLogger> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddModel _addPageModel;

        public AddModelTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockRepo = new Mock<IRepository<BackgroundCardType>>();
            _mockLogger = new Mock<IMyLogger>();
            _mockMapper = new Mock<IMapper>();

            _mockUnitOfWork.Setup(u => u.BackgroundCardTypeRepository).Returns(_mockRepo.Object);

            _addPageModel = new AddModel(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object)
            {
                BackgroundViewModel = new UpsertBackgroundViewModel()
            };
        }

        [Fact]
        public async Task OnGetAsync_BackgroundCardExists_PopulatesTheDropDown()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllAsync())
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
            _mockRepo.Setup(r => r.GetAllAsync())
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
            _mockRepo.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<BackgroundCardType, bool>>>()))
                    .ReturnsAsync(false);

            // Act
            var result = await _addPageModel.OnPostAsync();

            // Assert
            Assert.False(_addPageModel.ModelState.IsValid);
            Assert.IsType<PageResult>(result);
        }
    }
}
