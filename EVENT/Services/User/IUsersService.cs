public interface IUsersService
{
    public Task<string?> Registration(UserRegistrationModel body);
    public Task<string?> Authorization(UserAuthorizationModel body);
}