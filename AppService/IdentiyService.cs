﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Service;
using Entity;
using Generic.Core.Repository;
using Generic.Core.Service;
using Generic.Infracstructure.Services;


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
    }
}
