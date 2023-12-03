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

    public class AccessRecord
    {
        [Key]
        public int AccessRecordId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        // Relationship with User
        public int UserId { get; set; }
        public virtual Login Login { get; set; }

        // Relationship with Tokens
        public virtual Token Token { get; set; }
    }

}
