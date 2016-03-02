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

            FormIndex =1;
            var redirect =  Sections.Where(s => s.DisplayOrder.Equals(FormIndex)).FirstOrDefault().AspxPath;

            ancPayment.HRef = Sections.Where(s => s.DisplayOrder.Equals(FormIndex)).FirstOrDefault().AspxPath;
        }
    }
}