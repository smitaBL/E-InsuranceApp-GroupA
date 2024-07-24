using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Exceptions
{
    public class AgentException:Exception
    {
        public AgentException(string message) : base(message) { }
    }
}
