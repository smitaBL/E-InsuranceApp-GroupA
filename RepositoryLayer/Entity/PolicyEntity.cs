using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class PolicyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PolicyID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public CustomerEntity Customer { get; set; }

        [ForeignKey("Scheme")]
        public int SchemeID { get; set; }
        public SchemeEntity Scheme { get; set; }

        [Required]
        public string PolicyDetails { get; set; }

        [Required]
        public double Premium { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        [Required]
        public int MaturityPeriod { get; set; }

        [Required]
        public DateTime PolicyLapseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<PaymentEntity> Payments { get; set; } 
        public ICollection<CommissionEntity> Commissions { get; set; }
    }
}
