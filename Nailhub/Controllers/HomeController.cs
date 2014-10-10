using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Core.UnitOfWork;

namespace Nailhub.Controllers
{
    public class HomeController : Controller
    {
        private IAccountService account;
        private IUnitOfWorkAsync uowa;

        public HomeController(IUnitOfWorkAsync _uowa, IAccountService _account) {
            this.uowa = _uowa;
            this.account = _account;
        }
        public ActionResult Index()
        {
            var temp = account.Query().Get().SingleOrDefault();
            return View(
                temp
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