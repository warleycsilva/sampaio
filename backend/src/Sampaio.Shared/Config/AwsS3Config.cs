namespace Sampaio.Shared.Config
{
    public class AwsS3Config
    {
        public string AccessKeyId { get; set; }

        public string SecretAccessKey { get; set; }

        public string Region { get; set; }

        public string BucketName { get; set; } 
        
        public string AvatarsPath { get; set; } 
        public string ContractsPath { get; set; } 
        
        public string ClientsLogosPath { get; set; }
        
        public string BucketBaseUrl { get; set; }

        public string BuildAvatarPath(string key) => $"{AvatarsPath}/{key}";
        
        public string BuildClientLogoPath(string key) => $"{AvatarsPath}/{key}";
        public string BuildContractPath(string key) => $"{AvatarsPath}/{key}";
    }
}