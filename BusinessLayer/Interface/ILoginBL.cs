using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILoginBL
    {
        public Task<string> LoginAsync(LoginML model);
    }
}
