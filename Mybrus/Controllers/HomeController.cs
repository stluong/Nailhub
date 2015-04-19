using System.Threading;
using System.Web.Mvc;

namespace Mybrus.Controllers {
    public class HomeController : BaseController {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[AllowAnonymous]
        //public JsonResult SetCulture(string code) {
        //    var cultureInfo = TNTHelper.CultureManager.SetCulture(code);
        //    Thread.CurrentThread.CurrentCulture = cultureInfo;
        //    Thread.CurrentThread.CurrentUICulture = cultureInfo;
        //    return Json("success", JsonRequestBehavior.AllowGet);
        //}
    }
}
