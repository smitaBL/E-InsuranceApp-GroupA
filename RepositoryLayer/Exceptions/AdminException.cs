using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Exceptions
{
    public class AdminException : Exception
    {
        public AdminException(string message) : base(message)
        {
        }
    }
}
