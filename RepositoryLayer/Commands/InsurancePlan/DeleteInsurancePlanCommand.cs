using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.InsurancePlan
{
    public class DeleteInsurancePlanCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteInsurancePlanCommand(int id)
        {
            Id = id;
        }
    }
}
