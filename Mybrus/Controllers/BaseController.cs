using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stripe;
using TNTHelper;

namespace Mybrus.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        public enum MyResponse{
            success
            , error
        }
        
        public static string ImagePath
        {
            get
            {
                return "~/Content/images/mybrus/";
            }
        }

        public static decimal ShippingCost {
            get {
                return AppSettings.Get<decimal?>("ShippingCost") ?? 2.99m;
            }
        }


        #region Stripe
        protected static async Task<string> _GetTokenId()
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
        protected static async Task<StripeCharge> _ChargeCustomer(string stripeToken, string stripeEmail, decimal price, string description)
        {
            price = price > 30 ? price : price + ShippingCost;
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