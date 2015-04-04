using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mybrus.Controllers
{
    [Authorize]
    public class _Controller : Controller
    {
        // GET: _
        public ActionResult Index()
        {
            return View();
        }
    }
}