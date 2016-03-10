using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;
using Stripe;

namespace nChanger.WebUI.Secured
{
    public partial class StripePayment :AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Display();
        }

        private void Display()
        {
            if (Session["PKG"] != null)
            {
                var package = (Package) Session["PKG"];
                lblAmount.Text = package.Price.ToString("C");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["PKG"] != null)
            {
                var package = (Package) Session["PKG"];

                using (var dataContext = new nChangerDb())
                {
                    var user = dataContext.Users.Find(UserId);
                    if (user != null)
                    {
                        #region Charge....

                        var myCharge = new StripeChargeCreateOptions
                        {
                            Amount = (int) (package.Price * 100),
                            Currency = "usd",
                            Description = "Subscription Charges for " + package.PackageName + " package for "+UserName,
                            Source = new StripeSourceOptions()
                            { 
                                // set these properties if passing full card details (do not
                                // set these properties if you set TokenId)
                                Object = "card",
                                Number = txtCardNumber.Text,
                                ExpirationYear = txtExpieryYear.Text,
                                ExpirationMonth = ddlExpiryMonth.SelectedValue,
                                AddressCountry = user.Country, // optional
                                AddressLine1 =user.Address, // optional
                                AddressLine2 = user.Address2, // optional
                                AddressCity =user.City, // optional
                                AddressState = user.State, // optional
                                AddressZip =user.Zip, // optional
                                Name = UserName, // optional
                                Cvc = txtCVC.Text // optional
                            },
                             
                            Capture = true
                        };
                         
                        var chargeService = new StripeChargeService(ConfigurationManager.AppSettings["StripeApiKey"]);
                        var stripeCharge = chargeService.Create(myCharge);

                        #endregion Charge....

                        #region Log Entry... 
                        
                        dataContext.UserPayments.Add(new UserPayment
                        {
                            Id=Guid.NewGuid(),
                            Amount=(decimal) stripeCharge.Amount,
                            Currency=stripeCharge.Currency,
                            Status=stripeCharge.Status,
                            FailureCode=stripeCharge.FailureCode,
                            FailureMessage=stripeCharge.FailureMessage,
                            PackageId=package.Id,
                            PaymentDate=stripeCharge.Created,
                            UserId=UserId,
                            EntryIP=CommonFunctions.GetIpAddress(),
                            EntryId=UserId,
                            
                        });

                        dataContext.SaveChanges();

                        #endregion Log Entry...

                        #region Response...

                        Session.Add("PMS",stripeCharge);
                         
                        Response.Redirect("PaymentResponse.aspx");

                        #endregion Response...

                    }
                }
            }

        }
    }
}