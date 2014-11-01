using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Service;
using CFEntity.Models;
using TNT.Core.Repository;
using TNT.Infracstructure.Services;



namespace AppService
{
    public class IdentiyService : Service<AspNetUser>, IIdentityService
    {
        private readonly IRepositoryAsync<AspNetUser> rpoUser;
        public IdentiyService(IRepositoryAsync<AspNetUser> _rpoUser)
            : base(_rpoUser)
        {
            this.rpoUser = _rpoUser;
        }

        public string GetMessage()
        {
            return "I am a message :)";
        }
    }
}
