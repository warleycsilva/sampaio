using System.ComponentModel.DataAnnotations;

namespace Sampaio.Shared.ValueObjects
{
    public class FileInput
    {
        [Required]
        public byte[] Buffer { get; set; }

        [Required]
        public string Name { get; set; }

        public bool HasValue() => Buffer?.Length > 0;
    }
}