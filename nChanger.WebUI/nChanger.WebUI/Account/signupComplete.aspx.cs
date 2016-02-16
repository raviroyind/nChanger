using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Account
{
    public partial class signupComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                    ShowMessage(Request.QueryString["id"].ToLower());
            }
        }

        private void ShowMessage(string id)
        {
          
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbUser = dataContext.Users.Find(id);
                    if (dbUser != null)
                    {
                        var streamReader = new StreamReader(Server.MapPath(@"~/MailTemplates/signupComplete.html"));
                        var htmlContent = streamReader.ReadToEnd();
                        divMessage.InnerHtml = htmlContent.Replace("[USER]", dbUser.FirstName + " " + dbUser.LastName).Replace("[EMAIL]",dbUser.Email);
                        streamReader.Close();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}