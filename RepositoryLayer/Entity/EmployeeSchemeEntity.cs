using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class EmployeeSchemeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeSchemeID { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeID { get; set; }

        public EmployeeEntity? Employee { get; set; }

        [ForeignKey("Scheme")]
        public int? SchemeID { get; set; }

        public SchemeEntity? Scheme { get; set; }
    }
}
