using System.Threading.Tasks;
using System.Web.Mvc;


namespace Test.Web.Controllers
{
    public class ProductController : Controller
    {
        //private readonly IService<Product> _service;
        private readonly int _pageSize;

        public ProductController(/*IService<Product> service*/)
        {
            //_service = service;
            _pageSize = 10;
        }
        
        //public async Task<ActionResult> Index(int pageIndex = 1)
        //{
        //    ViewBag.PageIndex = pageIndex;
        //    var list = await new SelectList //_service.GetAllAsync(pageIndex, _pageSize);
        //    return View();
        //}
	}
}