using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Exceptions
{
    public class SchemeException : Exception
    {
        public SchemeException(string message) : base(message) { }
    }
}
