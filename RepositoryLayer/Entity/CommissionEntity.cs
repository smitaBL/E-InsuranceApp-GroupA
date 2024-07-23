using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CommissionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommissionID { get; set; }

        [ForeignKey("InsuranceAgent")]
        public int AgentID { get; set; }
        public InsuranceAgentEntity InsuranceAgent { get; set; }

        [ForeignKey("Policy")]
        public int PolicyID { get; set; }
        public PolicyEntity Policy { get; set; }

        [Required]
        public double CommissionAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
