namespace Deve.Tests
{
    public abstract class TestBase<TDataType> where TDataType : IDataCommon
    {
        protected IFixtureData<TDataType> FixtureData { get; private set; }
        protected IFixtureDataLogged<TDataType> FixtureDataLogged { get; private set; }

        public TestBase(IFixtureData<TDataType> fixtureData, IFixtureDataLogged<TDataType> fixtureDataLogged)
        {
            FixtureData = fixtureData;
            FixtureDataLogged = fixtureDataLogged;
        }
    }
}