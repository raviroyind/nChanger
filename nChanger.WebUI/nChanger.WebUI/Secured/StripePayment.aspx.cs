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
                try
                {
                    var package = (Package)Session["PKG"];

                    using (var dataContext = new nChangerDb())
                    {
                        var user = dataContext.Users.Find(UserId);
                        if (user != null)
                        {
                            #region Charge....

                            var myCharge = new StripeChargeCreateOptions
                            {
                                Amount = (int)(package.Price * 100),
                                Currency = "usd",
                                Description = "Subscription Charges for " + package.PackageName + " package for " + UserName,
                                Source = new StripeSourceOptions()
                                {
                                    // set these properties if passing full card details (do not
                                    // set these properties if you set TokenId)
                                    Object = "card",
                                    Number = txtCardNumber.Text,
                                    ExpirationYear = txtExpieryYear.Text,
                                    ExpirationMonth = ddlExpiryMonth.SelectedValue,
                                    AddressCountry = user.Country, // optional
                                    AddressLine1 = user.Address, // optional
                                    AddressLine2 = user.Address2, // optional
                                    AddressCity = user.City, // optional
                                    AddressState = user.State, // optional
                                    AddressZip = user.Zip, // optional
                                    Name = UserName, // optional
                                    Cvc = txtCVC.Text, // optional
                                    ReceiptEmail = user.Email
                                },

                                Capture = true
                            };

                            var stripeCharge = new StripeCharge();
                            try
                            {
                                var chargeService = new StripeChargeService(ConfigurationManager.AppSettings["StripeApiKey"]);
                                stripeCharge = chargeService.Create(myCharge);

                                #region Log Entry... 

                                dataContext.UserPayments.Add(new UserPayment
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = (decimal)(stripeCharge.Amount / 100),
                                    Currency = stripeCharge.Currency,
                                    Status = stripeCharge.Status,
                                    FailureCode = stripeCharge.FailureCode,
                                    FailureMessage = stripeCharge.FailureMessage,
                                    PackageId = package.Id,
                                    PaymentDate = stripeCharge.Created,
                                    UserId = UserId,
                                    EntryIP = CommonFunctions.GetIpAddress(),
                                    EntryId = UserId,
                                });


                                if (stripeCharge.Status.ToLower().Equals("succeeded"))
                                    dataContext.Database.ExecuteSqlCommand("UPDATE Users SET IsPaidMember='1' WHERE UserId='" +
                                                                           UserId + "'");
                                 
                                #endregion Log Entry...
                            }
                            catch(Exception error)
                            {
                                dataContext.UserPayments.Add(new UserPayment
                                {
                                    Id = Guid.NewGuid(),
                                    Amount = (decimal)package.Price,
                                    Currency = "usd",
                                    Status = "Failed",
                                    FailureCode = stripeCharge.FailureCode,
                                    FailureMessage = error.Message,
                                    PackageId = package.Id,
                                    PaymentDate = DateTime.Now,
                                    UserId = UserId,
                                    EntryIP = CommonFunctions.GetIpAddress(),
                                    EntryId = UserId,
                                });
                            }

                            #endregion Charge....

                            dataContext.SaveChanges();

                            #region Response...

                            Session.Add("PMS", stripeCharge);

                            Response.Redirect("PaymentResponse.aspx");

                            #endregion Response...

                        }
                    }
                }
                catch (Exception exception)
                {
                    
                }
               
            }

        }
    }
}