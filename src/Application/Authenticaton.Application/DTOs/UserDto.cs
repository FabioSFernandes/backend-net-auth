using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    [Serializable]
    public class UserDto
    {
        public int UserId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(15)]
        public string SecondaryPhone { get; set; }

        [StringLength(100)]
        public string Contact { get; set; }

        [StringLength(20)]
        public string GOVID { get; set; }

        [StringLength(20)]
        public string FISCALID { get; set; }

        [StringLength(20)]
        public string CORPORATEID { get; set; }

        public int UserType { get; set; }
    }
}
