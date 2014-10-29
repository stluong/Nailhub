﻿using System.Threading.Tasks;
using System.Web.Mvc;
using AppCore.Service;
using AppService;


namespace Test.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IIdentityService identity;

        
        public ProductController(IIdentityService _identity)
        {
            this.identity = _identity; 
        }
        
        //public async Task<ActionResult> Index(int pageIndex = 1)
        //{
        //    //ViewBag.PageIndex = pageIndex;
        //    //var user = await identity.Query().GetAsync();
        //    return View();
        //}
        public ActionResult Index() {
            var msg = identity.GetMessage();
            ViewBag.Msg = string.Format("Hello {0}, {}", identity.Query().Get(u => u.UserName), msg);
            return View();
        }
	}
}