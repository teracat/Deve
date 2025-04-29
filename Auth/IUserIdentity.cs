using Deve.Model;

namespace Deve.Auth
{
    /// <summary>
    /// User information used internally (can contain private information).
    /// When new properties are needed, you should also change the UserConverter methods.
    /// </summary>
    public interface IUserIdentity
    {
        public long Id { get; set; }

        public string Username {get;set;}

        public Role Role { get; set; }
    }
}
