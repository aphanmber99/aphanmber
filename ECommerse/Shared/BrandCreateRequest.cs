using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class BrandCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}