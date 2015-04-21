using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFColuc;
using TNT.Infracstructure.Services;
using CoLucCore;
using TNT.Core.Repository;

namespace CoLucService
{
    public class IdentityService : Service<AspNetUser>, IIdentityService
    {
        private readonly IRepositoryAsync<AspNetUser> rpoUser;
        public IdentityService(IRepositoryAsync<AspNetUser> _rpoUser)
            : base(_rpoUser)
        {
            this.rpoUser = _rpoUser;
        }
    }
}
