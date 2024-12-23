﻿namespace Deve.Auth
{
    public static class RoleConverter
    {
        public static string ToString(Role role)
        {
            return ((int)role).ToString();
        }

        public static Role ToRole(string? value, Role defaultRole = Role.User)
        {
            if (string.IsNullOrWhiteSpace(value) || !int.TryParse(value, out var role))
                return defaultRole;
            else
                return (Role)role;
        }
    }
}
