namespace Deve.Identity.Users;

internal static class Validator
{
    public static ResultBuilder CheckUserPassword(this ResultBuilder builder, string password) =>
        builder.CheckNotNullOrEmpty(new Field(password))
               .CheckStringMinLength(password, 8);

    public static ResultBuilder CheckUserFields(this ResultBuilder builder, string name, string username) =>
        builder.CheckNotNullOrEmpty(new Field(name),
                                    new Field(username))
               .CheckStringMaxLength(name, 50)
               .CheckStringMaxLength(username, 15);

    public static ResultBuilder CheckUserFields(this ResultBuilder builder, string name, string username, string password) =>
        builder.CheckUserFields(name, username)
               .CheckUserPassword(password);
}
