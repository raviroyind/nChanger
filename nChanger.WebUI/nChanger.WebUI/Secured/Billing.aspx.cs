using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nChanger.WebUI.Secured
{
    public partial class Billing : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ancPayment.InnerHtml = "Proceed to Pay "+ Request.QueryString["p1"];
            ancPayment.HRef = "GeneralQuestions.aspx?id=" + CurrentId;
        }
    }
}