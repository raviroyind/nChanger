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
    public partial class forgotpass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Text = Submit(txtRecover.Text);
            txtRecover.Text = string.Empty;
        }

        private string Submit(string id)
        {
            string message;
            try
            {
                if (CommonFunctions.CheckIfUserExists(id, id,out message, "GU"))
                {
                    using (var dataContext = new nChangerDb())
                    {
                        var user =
                            dataContext.Users.FirstOrDefault(
                                u => u.Email.ToLower().Equals(id) || u.UserId.ToLower().Equals(id));

                        if (user != null)
                        {
                            user.VerificationCode = Guid.NewGuid().ToString().ToLower();
                            dataContext.Users.AddOrUpdate(user);
                            dataContext.SaveChanges();
                            if (SendRecoveryMail(user))
                                message = "An email has been sent to you with instructions to reset your password.";
                            else
                                message = "There was a problem sending recovery email. Please try again in some time or contact administrator.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }


        private bool SendRecoveryMail(User user)
        {
            var success = true;
            try
            {
                var streamReader = new StreamReader(Server.MapPath(@"~/MailTemplates/forgotPass.html"));
                var htmlContent = streamReader.ReadToEnd();
                var mailBody = htmlContent.Replace("[USER]", user.UserId).Replace("[LINK]", @"http://" + Request.Url.Host + @"/account/passwordRecovery.aspx?id=" + user.VerificationCode);
                streamReader.Close();

                success = CommonFunctions.SendMail(user.Email, "[Name Changer] Password reset request", true, mailBody, true);
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
    }
}