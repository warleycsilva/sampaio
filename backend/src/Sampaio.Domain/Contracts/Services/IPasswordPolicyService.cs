namespace Sampaio.Domain.Contracts.Services
{
    public interface IPasswordPolicyService
    {
        string GeneratePassword(int minLength);

        bool PasswordIsValid(int minLength,
            string name,
            string email,
            string password);
    }
}