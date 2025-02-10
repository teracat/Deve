using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api
{
    public abstract class TestApiBaseGet<TEntryPoint> : TestApiBase<TEntryPoint> where TEntryPoint : class
    {
        #region Properties
        protected abstract string Path { get; }

        protected virtual string ValidCriteriaParameterName => "name";
        protected virtual long ValidId => TestsConstants.DefaultValidId;
        protected virtual long InvalidId => TestsConstants.DefaultInvalidId;
        #endregion

        #region Constructor
        public TestApiBaseGet(FixtureApiClients<TEntryPoint> fixture)
            : base(fixture)
        {
        }
        #endregion

        #region GetList
        [Fact]
        public async Task GetList_EmptyCriteria_SuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.GetAsync(Path);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetList_Parameter_SuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.GetAsync(Path + $"?{ValidCriteriaParameterName}=aa");

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_Zero_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.GetAsync(Path + "0");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Get_ValidId_SuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.GetAsync(Path + ValidId);

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion
    }
}