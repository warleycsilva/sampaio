using System.Collections.Generic;

namespace Sampaio.Shared.Constants
{
    public class ContentTypes
    {
        
        public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8";
        public const string Pdf = "application/pdf";
        public const string Doc = "application/msword";
        public const string DocX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string Html = "text/html";
        public const string XwwwFormUrlendoded = "application/x-www-form-urlencoded";
        public const string Json = "application/json";
        public const string Csv = "text/csv";
        public const string Png = "image/png";
        public const string Jpg = "image/jpg";
        public const string Jpeg = "image/jpeg";
        public const string Tiff = "image/tiff";
        public const string JavaScript = "application/javascript";
        public const string Svg = "image/svg+xml";
        public const string Gif = "image/gif";
        public const string Ico = "image/x-icon";
        public const string Text = "text/plain";

        public static IReadOnlyCollection<string> Images => new[]
        {
            Png, Jpg, Jpeg
        };
    }
}