using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILoginRL
    {
        public Task<string> LoginAsync(LoginML model);
    }
}
