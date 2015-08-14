using System;
using System.Collections.Generic;
using System.IO;
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
            return View(new xProduct { langid = (int?)int.Parse(Language.Lang.LangId) });
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int pid, int lid)
        {
            return View(this.prod.GetXProducts(pid, lid).FirstOrDefault());
        }
        // POST: ProductController/Edit/5
        [HttpPost]
        public async Task<ActionResult> CrudAsync(xProduct prod)
        {
            var result = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    this.prod.Crud(prod);
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
        [HttpPost]
        public async Task<ActionResult> UploadAsync()
        {
            var myResult = MyResponse.success.ToString();
            if (Request.Files.AllKeys.Any())
            {
                var httpPostedFile = Request.Files["uploadingImage"];
                if (httpPostedFile != null)
                {
                    var fileSavePath = Path.Combine(
                        Server.MapPath(AppSettings.Get<string>("Upload"))
                        , httpPostedFile.FileName
                    );
                    await Task.Run(() =>
                    {
                        try
                        {
                            httpPostedFile.SaveAs(fileSavePath);
                        }
                        catch (Exception ex) {
                            myResult = MyResponse.error.ToString();
                            Mailing.SendException(ex);
                        }
                        
                    });
                    
                }
            }
            return Json(myResult, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DeleteImageAsync(int prdid, string img)
        {
            var result = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    this.prod.DeleteImage(prdid, img);
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
        public async Task<ActionResult> SetImageAsync(int prdid, string img) {
            var result = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    this.prod.SetImage(prdid, img);
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
                    TNTHelper.Mailing.SendMail(updatingOrder.CustComment ?? string.Empty
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
