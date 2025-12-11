using Deve.Dto;

namespace Deve.Auth.Converters
{
    /// <summary>
    /// Provides conversion methods between the <see cref="Role"/> enum and string representations.
    /// </summary>
    public static class RoleConverter
    {
        /// <summary>
        /// Converts a <see cref="Role"/> enum value to its string representation.
        /// </summary>
        /// <param name="role">The role to convert.</param>
        /// <returns>The string representation of the role's integer value.</returns>
        public static string ToString(Role role) => ((int)role).ToString();

        /// <summary>
        /// Converts a string representation of a role to a <see cref="Role"/> enum value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="defaultRole">The default role to return if conversion fails.</param>
        /// <returns>
        /// The corresponding <see cref="Role"/> enum value if conversion is successful;
        /// otherwise, returns <paramref name="defaultRole"/>.
        /// </returns>
        public static Role ToRole(string? value, Role defaultRole)
        {
            if (string.IsNullOrWhiteSpace(value) || !int.TryParse(value, out var role))
            {
                return defaultRole;
            }
            else
            {
                return (Role)role;
            }
        }

        /// <summary>
        /// Converts a string representation of a role to a <see cref="Role"/> enum value.
        /// Defaults to <see cref="Role.User"/> if conversion fails.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>
        /// The corresponding <see cref="Role"/> enum value if conversion is successful;
        /// otherwise, returns <see cref="Role.User"/>.
        /// </returns>
        public static Role ToRole(string? value) => ToRole(value, Role.User);
    }
}