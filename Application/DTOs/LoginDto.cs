using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    
    public class LoginDto
    {
        //[Required]
        //[StringLength(50)]
        public string UserId { get; set; }

        //[Required]
        //[StringLength(100)]
        public string Password { get; set; }
    }
}
