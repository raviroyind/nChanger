using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Secured
{
    public partial class dashboard :AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                GenerateResponsiveTable();
        }


        private void GeneratePriceTable()
        {
            using (var dataContext = new nChangerDb())
            {
                var packageList = dataContext.Packages.Where(p => p.IsActive).ToList();
                var iCount = 0;
                foreach (var package in packageList)
                {
                    var sbBuilder = new StringBuilder();

                    sbBuilder.Append("<div class=\"col-md-3 col-sm-6 col-xs-12 float-shadow\">");
                    sbBuilder.Append("<div class=\"price_table_container\">");
                    sbBuilder.Append("<div class=\"price_table_heading\">" + package.PackageName + "</div>");
                    sbBuilder.Append("<div class=\"price_table_body\">");
                    sbBuilder.Append("<div class=\"price_table_row cost " + GetHeader(iCount) + "-bg\"><strong>" + package.Price.ToString("C") + "</strong><span>/MONTH</span></div>");

                    foreach (var feature in package.PackageFeatures)
                    {
                        sbBuilder.Append("<div class=\"price_table_row\">" + feature.FeatureName + "</div>");
                    }

                    sbBuilder.Append("</div>");

                    sbBuilder.Append("<a href = \"Account/signup.aspx?pid=" + package.Id + "\" class=\"btn btn-" + GetHeader(iCount) + " btn-lg btn-block\">Sign Up</a>");
                    sbBuilder.Append("</div>");
                    sbBuilder.Append("</div>");

                    rowPackage.InnerHtml += sbBuilder.ToString();
                    iCount++;
                }
            }
             
        }

        private void GenerateResponsiveTable()
        {
            using (var dataContext = new nChangerDb())
            {
                var packageList = dataContext.Packages.Where(p => p.IsActive).OrderBy(p => p.Price) .ToList();
                var iCount = 0;
                foreach (var package in packageList)
                {
                    var sbBuilder = new StringBuilder();

                    sbBuilder.Append("<div class=\"package brilliant"+GetHeader(iCount)+"\">");
                    sbBuilder.Append("<div class=\"name\">"+ package.PackageName + "<br/></div>");
                    sbBuilder.Append("<div class=\"price\">"+ package.Price.ToString("C")+ "</div>");
                    sbBuilder.Append("<div class=\"trial\">Available for a month</div>");
                    sbBuilder.Append("<hr class=\"ui divider header\"> ");
                    sbBuilder.Append("<ul>");

                    foreach (var feature in package.PackageFeatures)
                    {
                        sbBuilder.Append("<li>");
                        sbBuilder.Append("<i class=\"ui check icon green small\"></i>");
                        sbBuilder.Append("<strong>"+feature.FeatureName+"</strong>");
                        sbBuilder.Append("</li>");
                    }

                    sbBuilder.Append("</ul>");
                     
                    sbBuilder.Append("<hr class=\"ui divider header\"> ");
                    sbBuilder.Append("<div>");
                    sbBuilder.Append("<button class=\"large ui " + GetButtonColor(iCount) + " button fluid\"><i class=\"ui check circular icon small\"></i>Select");
                    
                    sbBuilder.Append("</button>");
                    sbBuilder.Append("</div>");
                    sbBuilder.Append("</div>");
                    divWrapper.InnerHtml += sbBuilder.ToString();
                    iCount++;
                }
            }
             
        }
        

        private static string GetButtonColor(int iCount)
        {
            var retVal = string.Empty;
            switch (iCount.ToString())
            {
                case "0":
                    retVal = "teal";
                    break;
                case "1":
                    retVal = "yellow";
                    break;
                case "2":
                    retVal = "dryred";
                    break;
                case "3":
                    retVal = "slate";
                    break;
            }

            return retVal;
        }
        private static string GetHeader(int iCount)
        {
            var retVal = string.Empty;
            switch (iCount.ToString())
            {
                case "0":
                    retVal = "";
                    break;
                case "1":
                    retVal = "1";
                    break;
                case "2":
                    retVal = "2";
                    break;
                case "3":
                    retVal = "3";
                    break;
            }

            return retVal;
        }

    }
}