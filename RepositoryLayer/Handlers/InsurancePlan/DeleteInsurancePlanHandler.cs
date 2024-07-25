using MediatR;
using RepositoryLayer.Commands.InsurancePlan;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.InsurancePlan
{
    public class DeleteInsurancePlanHandler : IRequestHandler<DeleteInsurancePlanCommand>
    {
        private readonly IInsurancePlanRL insurancePlanRL;

        public DeleteInsurancePlanHandler(IInsurancePlanRL insurancePlanRL)
        {
            this.insurancePlanRL = insurancePlanRL;
        }

        public async Task<Unit> Handle(DeleteInsurancePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await insurancePlanRL.DeleteInsurancePlanByIdAsync(request.Id);

                return Unit.Value;
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }
    }
}
