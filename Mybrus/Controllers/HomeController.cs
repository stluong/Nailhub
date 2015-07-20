using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using CoLucCore;
using EFColuc;
using Stripe;
using TNTHelper;
using System.Linq;

namespace Mybrus.Controllers {
    public class HomeController : BaseController {

        private readonly IProductService prod;
        private const string sssQuickCart = "mySSSQuickCart";

        public HomeController(IProductService _prod)
        {
            this.prod = _prod;
        }
        public ActionResult Index()
        {
            ViewBag.splProduct = this.prod.GetSpecialProduct(langId: int.Parse(Language.Lang.LangId));
            return View(
                this.prod.GetXProducts(langId: Language.Lang.LangId.ToNullable<int>())
                    .GroupBy(p => p.productid, (p, e) => e.FirstOrDefault())
            );
        }
        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TermsofUse()
        {
            return View();
        }
        public ActionResult Policy() {
            return View();
        }
        public ActionResult Detail(int id = 1) {
            var prdDetails = this.prod.GetXProducts(id, Language.Lang.LangId.ToNullable<int>());
            ViewBag.prdDetail = prdDetails.GroupBy(p => p.productid, (p, e) => e.FirstOrDefault());
            ViewBag.prdSize = prdDetails
                .Select(p => new SelectListItem
                {
                    Value = p.size.ToString(),//p.productid.ToString(),
                    Text = p.size.ToString(),
                }).ToArray()
            ;

            return View(this.prod.GetXProducts(langId: Language.Lang.LangId.ToNullable<int>())
                .GroupBy(p => p.productid, (p, e) => e.FirstOrDefault())
            );
        }
        public async Task<ActionResult> Charge(string stripeToken, string stripeEmail, xProduct prod)
        {
            try
            {
                //stripeToken = await _GetTokenId();
                var stripeCharge = await _ChargeCustomer(stripeToken, stripeEmail, prod.price, prod.description);
                if (stripeCharge.Status == "succeeded" && stripeCharge.Paid)
                {
                    Mailing.SendMail(stripeEmail, "Mybrus", "Thank you for ordering!! Your order will be sent asap.");
                    return Json(stripeCharge.Status, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception e)
            {
                //Do something with this exception
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Charges(string stripeToken, string stripeEmail, List<xProduct> prods)
        {
            try
            {
                //stripeToken = await _GetTokenId();
                var totalcharge = prods.Sum(p => p.price * p.quantity ?? 1);
                var description = string.Format("Order for product: {0}"
                    , string.Join(", ", prods.Select(p => p.productid))
                );
                var stripeCharge = await _ChargeCustomer(stripeToken, stripeEmail, totalcharge, description);
                if (stripeCharge.Status == "succeeded" && stripeCharge.Paid)
                {
                    Mailing.SendMail(stripeEmail, "Mybrus", "Thank you for ordering!! Your order will be sent asap.");
                    return Json(stripeCharge.Status, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                //Do something with this exception
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }


        public ActionResult QuickCart() {
            IEnumerable<xProduct> myCart = (IEnumerable<xProduct>)Session[sssQuickCart];
            return PartialView(myCart);
        }
        public ActionResult CartDetail()
        {
            IEnumerable<xProduct> myCart = (IEnumerable<xProduct>)Session[sssQuickCart];
            return PartialView(myCart);
        }
        public ActionResult Cart() {
            return View();
        }
        public ActionResult Cc() {
            return View();
        }
        public JsonResult UpdateCart(List<xProduct> prods)
        {
            try
            {
                Session[sssQuickCart] = prods;
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //throw
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddCart(xProduct prod){
            try
            {
                List<xProduct> myCart = (List<xProduct>)Session[sssQuickCart] ?? new List<xProduct>();
                //Add quantity 1 for quick cart
                prod.quantity = 1;
                myCart.Add(prod);
                Session[sssQuickCart] = myCart;
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) { 
                //throw
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RemoveCart(xProduct prod){
            try
            {
                List<xProduct> myCart = (List<xProduct>)Session[sssQuickCart];
                Session[sssQuickCart] = myCart
                    .Where(p=>p.productid != prod.productid)
                    .ToList()
                ;
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //throw
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            
        }

        #region Helper
        [Obsolete("Use stripe checkout for faster way!!")]
        private static async Task<string> _GetTokenId()
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var myToken = new StripeTokenCreateOptions
                {
                    Card = new StripeCreditCardOptions
                    {
                        Number = "4242424242424242",
                        Cvc = "123",
                        ExpirationMonth = "07",
                        ExpirationYear = "17"
                    }
                };

                var tokenService = new StripeTokenService();
                var stripeToken = tokenService.Create(myToken);

                return stripeToken.Id;
            });
        }
        private static async Task<StripeCharge> _ChargeCustomer(string stripeToken, string stripeEmail, decimal price, string description)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var myCharge = new StripeChargeCreateOptions
                {
                    Amount = (int)(price * 100),
                    Currency = "USD",
                    Description = description,
                    ReceiptEmail = stripeEmail,
                    Source = new StripeSourceOptions { TokenId = stripeToken }
                };

                var chargeService = new StripeChargeService();
                var stripeCharge = chargeService.Create(myCharge);

                return stripeCharge;
            });
        }

        #endregion
    }
}
