using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.ViewModels;
using Deve.Tests.Maui.Fixtures;
using Moq;

namespace Deve.Tests.Maui
{
    public abstract class TestListDataViewModel : IClassFixture<FixtureMaui>
    {
        private readonly FixtureMaui _fixture;

        protected TestListDataViewModel(FixtureMaui fixture)
        {
            _fixture = fixture;
        }

        protected abstract ListDataViewModel CreateViewModel(INavigationService navigationService, Internal.Data.IData data);

        [Fact]
        public async Task Initialization_ValidAuth_HasNoError()
        {
            var viewModel = CreateViewModel(_fixture.NavigationService.Object, _fixture.DataValidAuth);

            await viewModel.Initialization;

            Assert.False(viewModel.HasError);
        }

        [Fact]
        public async Task Initialization_ValidAuth_DataNotNull()
        {
            var viewModel = CreateViewModel(_fixture.NavigationService.Object, _fixture.DataValidAuth);

            await viewModel.Initialization;

            Assert.NotNull(viewModel.ListData);
        }

        [Fact]
        public async Task Initialization_ValidAuth_DataNotNullAndHasItems()
        {
            var viewModel = CreateViewModel(_fixture.NavigationService.Object, _fixture.DataValidAuth);

            await viewModel.Initialization;

            Assert.NotNull(viewModel.ListData);
            Assert.NotEmpty(viewModel.ListData);
        }

        [Fact]
        public async Task SelectedData_Null_NoException()
        {
            var viewModel = CreateViewModel(_fixture.NavigationService.Object, _fixture.DataValidAuth);
            await viewModel.Initialization;

            var exception = Record.Exception(() => viewModel.SelectedData = null);

            Assert.Null(exception);
        }

        [Fact]
        public async Task SelectedData_NotNull_NavigatesToDetails()
        {
            // We use a new instance so other tests does not interfere with this one
            var navigationService = new Mock<INavigationService>();
            var viewModel = CreateViewModel(navigationService.Object, _fixture.DataValidAuth);
            await viewModel.Initialization;
            var selectedItem = viewModel.ListData!.First();

            viewModel.SelectedData = selectedItem;

            navigationService.Verify(x => x.NavigateToAsync("details", new NavigationParameters
            {
                { nameof(BaseDetailsViewModel.Id), selectedItem.Id }
            }),
            Times.Once);
        }
    }
}