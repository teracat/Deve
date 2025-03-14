﻿using Moq;
using Deve.Authenticate;
using Deve.Core;
using Deve.Internal.Data;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Tests.Maui.Fixtures
{
    public class FixtureMaui : FixtureCommon
    {
        public IData DataNoAuth { get; }
        public IData DataValidAuth { get; }

        public Mock<INavigationService> NavigationService { get; private set; }

        public FixtureMaui()
        {
            DataNoAuth = new CoreMain(DataSource, Auth, Options);
            DataValidAuth = new CoreMain(DataSource, Auth, Options);

            NavigationService = new Mock<INavigationService>();
        }

        public override async Task InitializeAsync()
        {
            await DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        public override Task DisposeAsync()
        {
            DataNoAuth.Dispose();
            DataValidAuth.Dispose();
            return Task.CompletedTask;
        }
    }
}