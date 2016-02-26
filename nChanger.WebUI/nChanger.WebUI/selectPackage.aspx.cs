using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI
{
    public partial class selectPackage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Session.Clear();

                // clear authentication cookie
                var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "")
                {
                    Expires = DateTime.Now.AddYears(-1)
                };
                Response.Cookies.Add(cookie1);

                // clear session cookie
                var cookie2 = new HttpCookie("ASP.NET_SessionId", "") { Expires = DateTime.Now.AddYears(-1) };
                Response.Cookies.Add(cookie2);
                GeneratePriceTable();
            }
        }

        private void GeneratePriceTable()
        {

            using (var dataContext=new nChangerDb())
            {
                var packageList = dataContext.Packages.Where(p => p.IsActive).ToList();
                var iCount = 0;
                foreach (var package in packageList)
                {
                   var sbBuilder = new StringBuilder();

                    sbBuilder.Append("<div class=\"col-md-3 col-sm-6 col-xs-12 float-shadow\">");
                    sbBuilder.Append("<div class=\"price_table_container\">");
                    sbBuilder.Append("<div class=\"price_table_heading\">"+package.PackageName+"</div>");
                    sbBuilder.Append("<div class=\"price_table_body\">");
                    sbBuilder.Append("<div class=\"price_table_row cost "+ GetHeader(iCount) + "-bg\"><strong>" + package.Price.ToString("C")+"</strong><span>/MONTH</span></div>");

                    foreach (var feature in package.PackageFeatures)
                    {
                        sbBuilder.Append("<div class=\"price_table_row\">"+ feature.FeatureName +"</div>");
                    }
                     
                    sbBuilder.Append("</div>");
                     
                    sbBuilder.Append("<a href = \"Account/signup.aspx?pid="+ package.Id +"\" class=\"btn btn-" + GetHeader(iCount) + " btn-lg btn-block\">Sign Up</a>");
                    sbBuilder.Append("</div>");
                    sbBuilder.Append("</div>");
                     
                    rowPackage.InnerHtml += sbBuilder.ToString();
                    iCount++;
                }
            }

            
        }

        private static string GetHeader(int iCount)
        {
            var retVal = string.Empty;
            switch (iCount.ToString())
            {
                case "0":
                    retVal = "success";
                    break;
                case "1":
                    retVal = "warning";
                    break;
                case "2":
                    retVal = "info";
                    break;
                case "3":
                    retVal = "primary";
                    break;
                case "4":
                    retVal = "danger";
                    break;
            }

            return retVal;
        }

    }
}