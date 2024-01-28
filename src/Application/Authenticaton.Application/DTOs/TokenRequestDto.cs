using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    [Serializable]
    public class TokenRequestDto
    {
       // [Required]
        public string ProductId { get; set; }

        //[Required]
        public string AppId { get; set; }

        //[Required]
        public string AppSecret { get; set; }
    }
}
