using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class SchemeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchemeID { get; set; }

        [Required]
        [StringLength(100)]
        public string SchemeName { get; set; }

        [Required]
        public string SchemeDetails { get; set; }

        [ForeignKey("InsurancePlan")]
        public int PlanID { get; set; }
        public InsurancePlanEntity InsurancePlan { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<PolicyEntity> Policies { get; set; }
        public ICollection<EmployeeEntity> Employees { get; set; }
    }
}
