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
    public class Token
    {
        [Key]
        public int TokenId { get; set; }

        [Required]
        [StringLength(200)]
        public string Value { get; set; }

        [Required]
        [StringLength(200)]
        public string RefreshToken { get; set; }

        [Required]
        public int ExpirationInMinutes { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        // Relationship with AccessRecord
        public int AccessRecordId { get; set; }
        public int LoginId { get; set; }
        public virtual ICollection<AccessRecord> AccessRecords { get; set; }
        public virtual Login Login { get; set; }
    }

}
