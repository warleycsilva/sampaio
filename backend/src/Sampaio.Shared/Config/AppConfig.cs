namespace Sampaio.Shared.Config
{
    public class AppConfig
    {
        
        public string Logger { get; set; }

        public string Domain { get; set; }

        public string BaseUrl { get; set; }
        
        public string AppUrl { get; set; }
        
        public string CookieSessionName { get; set; }

        public bool RequireHttps { get; set; }
        public string FirebaseCredentials { get; set; }

    }
}