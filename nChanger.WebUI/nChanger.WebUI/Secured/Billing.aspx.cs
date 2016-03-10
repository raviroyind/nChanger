using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Secured
{
    public partial class Billing : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                if(Session["PKG"]!=null)
                    Session.Remove("PKG");

                if (Request.QueryString["id"] != null)
                {
                    var id = Guid.Parse(Request.QueryString["id"]);

                    using (var dataContext=new nChangerDb())
                    {
                       var package =  dataContext.Packages.Find(id);

                        ancPayment.InnerHtml = "Proceed to Pay " + package.Price.ToString("C");
                        ancPayment.HRef = "../Secured/StripePayment.aspx";
                        Session.Add("PKG",package);
                    }
                }
            }
        }
    }
}