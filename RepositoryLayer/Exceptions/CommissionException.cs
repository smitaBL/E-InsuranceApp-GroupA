using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Exceptions
{
    public class CommissionException:Exception
    {
        public CommissionException(string message):base(message) { }
    }
}
