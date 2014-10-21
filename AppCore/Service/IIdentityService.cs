using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Generic.Core.Repository;
using Generic.Core.Service;

namespace AppCore.Service
{
    public interface IIdentityService : IService<AspNetUser>
    {

    }
}
