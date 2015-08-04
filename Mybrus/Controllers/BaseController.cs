using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stripe;

namespace Mybrus.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        public const string CallSucess = "sucess";
        public const string CallError = "error";

        public static string ImagePath
        {
            get
            {
                return "~/Content/images/mybrus/";
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