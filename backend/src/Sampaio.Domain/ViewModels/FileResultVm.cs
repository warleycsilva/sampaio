namespace Sampaio.Domain.ViewModels
{
    public class FileResultVm: BaseVm
    {
        public byte[] Buffer { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}