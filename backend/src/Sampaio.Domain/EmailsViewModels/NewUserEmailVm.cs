namespace Sampaio.Domain.EmailsViewModels
{
    public class NewUserEmailVm
    {
        public NewUserEmailVm(string name, string email, string token, string tempPassword = null)
        {
            Name = name;
            Email = email;
            Token = token;
            TempPassword = tempPassword ?? "";
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Subject { get; set; }

        public string TempPassword { get; set; } = "";

    }
}