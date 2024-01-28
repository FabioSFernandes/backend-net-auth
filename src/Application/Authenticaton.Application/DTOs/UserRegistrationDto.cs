using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    [Serializable]
    public class UserRegistrationDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(15)]
        public string SecondaryPhone { get; set; }

        [StringLength(100)]
        public string Contact { get; set; }

        [StringLength(20)]
        public string RG { get; set; }

        [StringLength(11)]
        public string CPF { get; set; }

        [StringLength(14)]
        public string CNPJ { get; set; }
    }
}
