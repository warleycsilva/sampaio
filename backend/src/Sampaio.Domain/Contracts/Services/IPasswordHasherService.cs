namespace Sampaio.Domain.Contracts.Services
{
    public interface IPasswordHasherService
    {
        string Hash(string password);

        bool Check(string hash,
            string password);
    }
}