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
    public partial class verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                    ActivateUser(Request.QueryString["id"].ToLower());
            }
        }

        private void ActivateUser(string id)
        {
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbUser = dataContext.Users.FirstOrDefault(u => u.VerificationCode.ToLower().Equals(id));
                    if (dbUser != null)
                    {
                        dbUser.EmailVerified = true;
                        dbUser.IsActive = true;

                        dataContext.Users.AddOrUpdate(dbUser);
                        dataContext.SaveChanges();
                        ShowMessage(dbUser);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void ShowMessage(User user)
        {
            var streamReader = new StreamReader(Server.MapPath(@"~/MailTemplates/activation.html"));
            var htmlContent = streamReader.ReadToEnd();
            divMessage.InnerHtml = htmlContent.Replace("[USER]", user.FirstName + " "+ user.LastName);
            streamReader.Close();
        }
    }
}