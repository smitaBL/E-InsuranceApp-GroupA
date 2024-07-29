using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class PolicyStatusEntity
    {
        [Key]
        public int PolicyStatusID { get; set; }

        [ForeignKey("Policy")]
        public int PolicyID { get; set; }
        public PolicyEntity Policy { get; set; }

        [Required]
        [MaxLength(100)]
        public string Status { get; set; }
    }
}
