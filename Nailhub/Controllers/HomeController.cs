using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppCore.Service;
using Generic.Core.Identity;
using Generic.Core.UnitOfWork;

namespace Nailhub.Controllers
{
    public class HomeController : Controller
    {
        //private IIdentityService account;
        //private IUnitOfWorkAsync uowa;

        private IApplicationUserManager userMgr;

        public HomeController(/*IUnitOfWorkAsync _uowa, IIdentityService _account*/ IApplicationUserManager _userMgr) {
            //this.uowa = _uowa;
            //this.account = _account;
            this.userMgr = _userMgr;
        }
        public ActionResult Index()
        {
            var user = userMgr.FindByName("stluong"); 
            return View(
                //account.Query().Get().SingleOrDefault()
            );
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}