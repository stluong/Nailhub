using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppCore.Service;
using AppService;
using Autofac;
using Entity;
using Generic.Core.Service;
using Generic.Infracstructure.Services;

namespace Test
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder) {
            //builder.RegisterType(typeof(Service<AspNetUser>)).As(typeof(IService<AspNetUser>)).InstancePerRequest();
            builder.RegisterType(typeof(IdentiyService)).As(typeof(IIdentityService)).InstancePerRequest();
        }
    }
}