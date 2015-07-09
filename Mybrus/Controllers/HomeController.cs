using System.Collections;
using System.Threading;
using System.Web.Mvc;
using CoLucCore;

namespace Mybrus.Controllers {
    public class HomeController : BaseController {

        private readonly IProductService prod;

        public HomeController(IProductService _prod)
        {
            this.prod = _prod;
        }

        public ActionResult Index()
        {
            return View(this.prod.GetXProducts());
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
