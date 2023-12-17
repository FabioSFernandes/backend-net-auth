using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    [Serializable]
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<string> perfil { get; set; }
    }
}
