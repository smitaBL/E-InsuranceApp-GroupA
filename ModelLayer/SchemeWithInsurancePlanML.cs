using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class SchemeWithInsurancePlanML
    {
        public int SchemeID { get; set; }
        public string SchemeName { get; set; }
        public string SchemeDetails { get; set; }
        public double SchemePrice { get; set; }
        public double SchemeCover { get; set; }
        public int SchemeTenure { get; set; }
        public int PlanID { get; set; }
        public DateTime SchemeCreatedAt { get; set; }
        public string PlanName { get; set; }
        public string PlanDetails { get; set; }
        public DateTime PlanCreatedAt { get; set; }
    }
}
