using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Service;
using Entity;
using Generic.Core.Repository;
using Generic.Infracstructure.Services;


namespace AppService
{
    public class AccountService: Service<USER>, IAccountService
    {
        private IRepositoryAsync<USER> rpoUser;
        public AccountService(IRepositoryAsync<USER> _rpoUser) : base(_rpoUser) {
            this.rpoUser = _rpoUser;
        }
    }
}
