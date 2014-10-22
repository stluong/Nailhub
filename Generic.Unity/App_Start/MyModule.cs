using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Configuration;

namespace Generic
{
    /// <summary>
    /// Author: StevenLuong, 10/22/2014
    /// Description: Auto register all autofac modules with parameters on configuration file
    /// </summary>
    public class MyModule : Module
    {
        private readonly IList<Module> modules;
        /// <summary>
        /// Nullable modules
        /// </summary>
        /// <param name="modules"></param>
        public MyModule(IList<Module> modules)
        {
            this.modules = modules;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //Register default identity module
            builder.RegisterModule(new IdentityModule());
            
            //Register other modules if specific
            if (this.modules != null && this.modules.Count() > 0) {
                var appSetting = ConfigurationManager.AppSettings;
                var appKeys = appSetting.AllKeys;
                
                foreach (var module in modules)
                {
                    //Check if module has any properties setting on configuration
                    var keys = appKeys
                        .Where(k => k.Contains(string.Format("{0}.", module.GetType().Name.Trim())))
                        .ToList()
                    ;
                    if (keys.Count() > 0) { 
                        //module has settings on config
                        foreach (var key in keys) {
                            var keyTemp = key.Split('.');
                            var moduleName = keyTemp[0].Trim();
                            var propName = keyTemp[1].Trim();
                            var value = appSetting[key].Trim();

                            var moduleProperty = module.GetType().GetProperty(propName);
                            moduleProperty.SetValue(module, Convert.ChangeType(value, moduleProperty.PropertyType));
                        }
                    }
                    builder.RegisterModule(module);
                }
            }
            
        }
    }
}
