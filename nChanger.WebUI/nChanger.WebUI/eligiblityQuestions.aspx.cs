using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stripe.Infrastructure;
using Stripe;

namespace nChanger.WebUI
{
    public partial class eligiblityQuestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            #region Working....

            var myCharge = new StripeChargeCreateOptions
            {
                Amount =(int) (29.99 *100),
                Currency = "usd",
                Description = "Charge it like it's hot 2",
                Source = new StripeSourceOptions()
                {
                    // set this property if using a token


                    // set these properties if passing full card details (do not
                    // set these properties if you set TokenId)
                    Object = "card",
                    Number = "4242424242424242",
                    ExpirationYear = "2022",
                    ExpirationMonth = "10",
                    AddressCountry = "US", // optional
                    AddressLine1 = "24 Beef Flank St", // optional
                    AddressLine2 = "Apt 24", // optional
                    AddressCity = "Biggie Smalls", // optional
                    AddressState = "NC", // optional
                    AddressZip = "27617", // optional
                    Name = "Joe Meatballs 2", // optional
                    Cvc = "1223" // optional
                },
                //ApplicationFee = 25,
                Capture = true
            };

            // always set these properties

            // set this if you want to

            // setting up the card

            //set this property if using a customer
            //myCharge.CustomerId = "postman2021";

            // set this if you have your own application fees (you must have your application configured first within Stripe)

            // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later

            var chargeService = new StripeChargeService(ConfigurationManager.AppSettings["StripeApiKey"]);
            var stripeCharge = chargeService.Create(myCharge);
            

            #endregion Working....
        }
    }
}