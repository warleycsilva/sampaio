namespace Sampaio.Domain.EmailsViewModels
{
    public class ResetPasswordEmailVm
    {
        public ResetPasswordEmailVm(string name, string email, string token)
        {
            Name = name;
            Email = email;
            Token = token;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string Subject { get; set; }
    }
}