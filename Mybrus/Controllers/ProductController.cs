using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoLucCore;
using EFColuc;
using Mybrus.Models;
using TNT.Core.UnitOfWork;
using TNTHelper;

namespace Mybrus.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService prod;
        public ProductController(IProductService _prod) {
            this.prod = _prod;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var prods =
                //this.prod.GetProducts().ToList()
                this.prod.GetXProducts()
                .GroupBy(p => new { p.productid, p.langid })
                .Select(pl => pl.FirstOrDefault())
                .ToList()
            ;
            
            return View(prods);
        }

        // GET: ProductController/Details/5
        //public ActionResult Details(int id)
        //{
        //    var pd = this.prod.Find(id);
        //    return View(pd);
        //}

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.prodBrand = this.prod.GetBrands()
                .Select(b => new SelectListItem
                {
                    Value = b.BrandId.ToString(),
                    Text = b.Name,
                }).ToArray()
            ;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int pid, int lid)
        {
            return View(this.prod.GetXProducts(pid, lid).FirstOrDefault());
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditAsync(xProduct prod)
        {
            var result = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    // TODO: Add update logic here
                    this.prod.Update(prod);
                    result = MyResponse.success.ToString();
                }
                catch (Exception ex)
                {
                    Mailing.SendException(ex);
                    result = MyResponse.error.ToString();
                }
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Sale() {
            var orderings = this.prod.GetXOrder();
            return View(orderings);
        }

        public async Task<ActionResult> UpdateTracking(int orderId, string trackingNo)
        {
            try {
                await Task.Run(() => {
                    var updatingOrder = this.prod.UpdateTracking(orderId, trackingNo);
                    TNTHelper.Mailing.SendMail(updatingOrder.CustComment
                        , "Order Shipped"
                        , string.Format("Your order was shipped with this tracking no: {0}", updatingOrder.TrackingNo))
                    ;
                }); 

                return Json(MyResponse.success.ToString());
            }
            catch{
                return Json(MyResponse.error.ToString());
            }
            
        } 

        [AllowAnonymous]
        public ActionResult PtBrandMenu() {
            return PartialView(this.prod.GetBrands()
                .Select(b => new MdBrandMenu { 
                    BrandId = b.BrandId,
                    Name = b.Name
                })
                .ToList()
            );                 
        }
        
    }
}
