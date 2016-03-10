using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;
using Stripe;

namespace nChanger.WebUI.Secured
{
    public partial class PaymentResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Display();
        }

        private void Display()
        {
            if (Session["PMS"] != null)
            {
                var stripeCharge = (StripeCharge) Session["PMS"];
                 
                if (stripeCharge.Status.ToLower().Equals("succeeded"))
                {
                    divStatus.InnerHtml += "<h3 class='ui header green'>Payment Successful</h3>";
                    divStatus.InnerHtml += "<h4 class='ui header'>Please click on \"Continue\" button below to proceed.</h4>";

                    hypProceed.Visible = true;
                }
                else
                {
                    divStatus.InnerHtml += "<h3 class='ui header red'>Payment Un-successful</h3>";
                    divStatus.InnerHtml += "<h4 class='ui header red'>Error message"+stripeCharge.FailureMessage+"</h4>";

                    divStatus.InnerHtml += "<h4 class='ui header'>Please click on \"Retry Payment\" button below to try again.</h4>";
                    hypBack.Visible = true;
                }
            }
        }
    }
}