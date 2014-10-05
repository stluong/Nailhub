using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services;
using Entity;
using Infrastructure;
using Infrastructure.Repository;

namespace Services
{
    public class AccountService: Service<USER>, IAccountService
    {
        private IRepositoryAsync<USER> rpoUser;
        public AccountService(IRepositoryAsync<USER> _rpoUser) : base(_rpoUser) {
            this.rpoUser = _rpoUser;
        }
    }
}
