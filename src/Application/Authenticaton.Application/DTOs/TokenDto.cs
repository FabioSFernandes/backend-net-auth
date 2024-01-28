using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    [Serializable]
    public class TokenDto
    {
        //[Required]
        public string Token { get; set; }

        //[Required]
        public DateTime Expiration { get; set; }
    }
}
