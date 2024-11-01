namespace Deve.Auth
{
    public class UserAndUserToken
    {
        public User User { get; set; }
        public UserToken UserToken { get; set; }

        public UserAndUserToken(User user, UserToken userToken)
        {
            User = user;
            UserToken = userToken;
        }
    }
}
