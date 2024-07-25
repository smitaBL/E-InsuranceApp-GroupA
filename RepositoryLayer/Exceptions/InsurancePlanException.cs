using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Exceptions
{
    public class InsurancePlanException : Exception
    {
        public InsurancePlanException(string message) : base(message) { }
    }
}
