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
            ViewBag.prdDetail = this.prod.GetXProducts(id, Language.Lang.LangId.ToNullable<int>());
            return View(this.prod.GetXProducts(langId: Language.Lang.LangId.ToNullable<int>()));
        }

        public async Task<ActionResult> Charge(string stripeToken, string stripeEmail, xProduct chargingObject)
        {
            try
            {
                stripeToken = await _GetTokenId();
                var stripeCharge = await _ChargeCustomer(stripeToken, stripeEmail, chargingObject);
                if (stripeCharge.Status == "succeed" && stripeCharge.Paid)
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
                        ExpirationMonth= "07",
                        ExpirationYear = "17"
                    }
                };

                var tokenService = new StripeTokenService();
                var stripeToken = tokenService.Create(myToken);

                return stripeToken.Id;
            });
        }
        private static async Task<StripeCharge> _ChargeCustomer(string stripeToken, string stripeEmail, [Bind(Include = "productid, price, description")]xProduct chargingObject)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var myCharge = new StripeChargeCreateOptions
                {
                    Amount = (int)(chargingObject.price * 100),
                    Currency= "USD",
                    Description = chargingObject.description,
                    ReceiptEmail = stripeEmail,
                    Source = new StripeSourceOptions { TokenId = stripeToken }                
                };

                var chargeService = new StripeChargeService();
                var stripeCharge = chargeService.Create(myCharge);

                return stripeCharge;
            });
        }


        public ActionResult Cart() {
            return View();
        }

        public ActionResult Cc() {
            return View();
        }
    }
}
