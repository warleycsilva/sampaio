namespace Sampaio.Shared.Mail
{
    public class EmailConfig
    {
        public bool EnableSsl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string From { get; set; }

        public string FromName { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }        
    }
}
