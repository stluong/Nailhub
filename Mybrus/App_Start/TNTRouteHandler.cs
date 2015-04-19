using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mybrus
{
    public class TNTRouteHandler : MvcRouteHandler
    {
        private const string ckiLanguage = "mybruslang";
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var mybrusLang = requestContext.HttpContext.Request.Cookies[ckiLanguage];
            if (mybrusLang != null)
            {
                TNTHelper.CultureManager.SetCulture(mybrusLang.Value);
                requestContext.HttpContext.Request.Cookies.Remove(ckiLanguage);
            }
            return base.GetHttpHandler(requestContext);
        }
    }
}