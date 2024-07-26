using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Scheme
{
    public class CreateSchemeCommand : IRequest
    {
        public string SchemeName { get; set; }
        public string SchemeDetails { get; set; }
        public double SchemePrice { get; set; }
        public double SchemeCover { get; set; }
        public int SchemeTenure { get; set; }
        public int PlanID { get; set; }
        public int EmployeeId { get; set; }

        public CreateSchemeCommand(string SchemeName, string SchemeDetails, double SchemePrice, double SchemeCover, int SchemeTenure, int PlanID, int employeeId)
        {
            this.SchemeName = SchemeName;
            this.SchemeDetails = SchemeDetails;
            this.SchemePrice = SchemePrice;
            this.SchemeCover = SchemeCover;
            this.SchemeTenure = SchemeTenure;
            this.PlanID = PlanID;
            EmployeeId = employeeId;
        }

    }
}
