using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{


    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(15)]
        public string SecondaryPhone { get; set; }

        [StringLength(100)]
        public string Contact { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string GOVID { get; set; }

        [StringLength(20)]
        public string FISCALID { get; set; }

        [StringLength(20)]
        public string CORPORATEID { get; set; }

        [Required]
        public int UserType { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        // Relationships
        public virtual ICollection<Login> Logins { get; set; }
        
    }

  

}
