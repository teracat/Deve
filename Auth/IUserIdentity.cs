using Deve.Dto;

namespace Deve.Auth
{
    /// <summary>
    /// User information used internally (can contain private information).
    /// When new properties are needed, you should also change the UserConverter methods.
    /// </summary>
    public interface IUserIdentity
    {
        long Id { get; set; }

        string Username { get; set; }

        Role Role { get; set; }
    }
}
