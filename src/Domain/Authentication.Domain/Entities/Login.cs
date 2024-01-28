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

    public class Login
    {
        [Key]
        public int LoginId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        // Relationship with User
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

}
