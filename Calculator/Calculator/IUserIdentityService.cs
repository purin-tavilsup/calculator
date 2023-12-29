namespace Calculator
{
    public interface IUserIdentityService
    {
        bool ValidateUser(string userId, string userName);

        string GetUserName(string userId);
    }
}
