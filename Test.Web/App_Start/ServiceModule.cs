using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppCore.Service;
using AppService;
using Autofac;
using TNT.Core.Repository;
using TNT.Core.Service;
using TNT.Infracstructure.Services;
using TNT.Infrastructure.Repositories;

namespace Test.Web
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder) {
            //builder.RegisterType(typeof(Repository<AspNetUser>)).As(typeof(IRepositoryAsync<AspNetUser>)).InstancePerRequest();
            //builder.RegisterType(typeof(Service<AspNetUser>)).As(typeof(IService<AspNetUser>)).InstancePerRequest();
            //builder.RegisterType(typeof(IdentiyService)).As(typeof(IIdentityService)).InstancePerRequest();
        }
    }
}