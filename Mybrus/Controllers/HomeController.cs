using System.Collections;
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

        public ActionResult Detail(int productId = 1) {
            return View();
        }

        public ActionResult Cart() {
            return View();
        }

        public ActionResult Cc() {
            return View();
        }
    }
}
