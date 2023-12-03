using System.ComponentModel.DataAnnotations;

namespace TokenApi.Utils
{
    public class AppAuthSettings
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string AppId { get; set; }

        [Required]
        public string AppSecret { get; set; }
    }
}
