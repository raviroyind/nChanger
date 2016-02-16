using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;
using System.Data.Entity.Validation;
using System.IO;

namespace nChanger.WebUI.Account
{
    public partial class signup : System.Web.UI.Page
    {
        private string queryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdowns();
            }
        }

        private void BindDropdowns()
        {
             
            using (var dataContext = new nChangerDb())
            {
                ddlCountry.DataSource = dataContext.Countries.ToList();
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("--SELECT--", "SEL"));
            }
        }

        public bool Submit(out string returnMessage)
        {
            returnMessage = string.Empty;
            var success = true;
            try
            {
                var message = string.Empty;
                if (CommonFunctions.CheckIfUserExists(txtEmailId.Text, txtUserId.Text, out message,"GU"))
                {
                    lblMsg.Text = message;
                    success = false;
                    return success;
                }


                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = new User
                    {
                        Id = Guid.NewGuid(),
                        UserTypeId="GU",
                        UserId = txtUserId.Text,
                        Email = txtEmailId.Text,
                        Password = txtPassword.Text,
                        FirstName = txtFirstName.Text,
                        MiddleName = txtMiddleName.Text,
                        LastName = txtLastName.Text,
                        Phone = txtPhone.Text,
                        City = txtCity.Text,
                        State = ddlState.SelectedIndex == -1 ? txtState.Text : ddlState.SelectedValue,
                        Zip = txtZipCode.Text,
                        Country = ddlCountry.SelectedValue,
                        Address = txtAddressLine1.Text,
                        Address2 = txtAddressLine2.Text,
                        IP = CommonFunctions.GetIpAddress(),
                        IsActive = false,
                        EmailVerified = false,
                        VerificationCode = Guid.NewGuid().ToString(),
                        RegistrationDate = DateTime.Now
                    };

                    dataContext.Users.Add(dbEntry);
                    dataContext.SaveChanges();
                    queryId = dbEntry.UserId;
                    returnMessage = SendRegistrationMail(dbEntry) ? "SUCCESS" : "MAIL_ERROR";
                }

            }
            catch (DbEntityValidationException ex)
            {
                success = false;
                returnMessage = ex.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors).Aggregate(returnMessage, (current, ve) => current + (ve.PropertyName + " Error Msg:" + ve.ErrorMessage));
            }

            return success;
        }

        private bool SendRegistrationMail(User user)
        {
            var success = true;
            try
            {
                var streamReader = new StreamReader(Server.MapPath(@"~/MailTemplates/signup.html"));
                var htmlContent = streamReader.ReadToEnd();
                var mailBody = htmlContent.Replace("[USER]", user.FirstName).Replace("[LINK]", @"http://"+ Request.Url.Host+ @"/account/verification.aspx?id=" + user.VerificationCode);
                streamReader.Close();

                success = CommonFunctions.SendMail(user.Email, "Registration Complete", true, mailBody, true);
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string message;
            if (!Submit(out message))
                lblMsg.Text = message;
            else
                Response.Redirect("signupComplete.aspx?id="+ queryId, true);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            var password = txtPassword.Text;
            txtPassword.Attributes.Add("value", password);
            txtConfirm.Attributes.Add("value", password);

            if (ddlCountry.SelectedValue.Equals("US") || ddlCountry.SelectedValue.Equals("CA"))
            {
                var country = ddlCountry.SelectedValue;
                using (var dataContext = new nChangerDb())
                {
                    ddlState.DataSource = dataContext.States.Where(s => s.CountryId.Equals(country)).ToList();
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();

                    ddlState.Items.Insert(0, new ListItem("--SELECT--", "SEL"));
                    ddlState.CssClass = "ui fluid dropdown selection";

                    divDropState.Style.Add(HtmlTextWriterStyle.Display, "block");
                    divStateText.Style.Add(HtmlTextWriterStyle.Display, "none");

                }
            }
            else
            {
                divDropState.Style.Add(HtmlTextWriterStyle.Display, "none");
                divStateText.Style.Add(HtmlTextWriterStyle.Display, "block");
            }
        }
    }
}