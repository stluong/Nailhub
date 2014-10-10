using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingMVC4.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var student = new student();

            using (var context = new ManageStudentEntities()) {
                student = context.students.SingleOrDefault();
            }

            return View(student);
        }

    }
}
