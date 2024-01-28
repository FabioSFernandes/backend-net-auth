using System.ComponentModel.DataAnnotations;

namespace TokenMan.Jwt
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
