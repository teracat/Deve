﻿using Deve.Model;
using Deve.Internal.Model;

namespace Deve.Tests
{
    public class UserTests : User
    {
        public UserTests()
        {
            Id = 1;
            Name = "Tests User";
            IsActive = true;
            Username = "testsuser";
            PasswordHash = "";
            Joined = new DateTime(2024, 10, 28);
            Role = Role.User;
        }
    }
}
