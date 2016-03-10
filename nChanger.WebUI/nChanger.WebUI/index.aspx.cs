using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using nChanger.Core;
using nChanger.WebUI.UserControls;

namespace nChanger.WebUI
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSubmit.UniqueID;
        

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id"] != null)
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

                    

                    switch (Convert.ToString(Request.QueryString["id"]))
                    {
                        case "ua":
                            lblMsg.Text = "You are not authorized to use application!";
                            break;
                        case "lg":
                            lblMsg.Text = "You have sucessfully logged out!";
                            break;
                    }
                }
            }

            var ucLogin = (_topNav)this.Master.FindControl("_topNav1");
            ucLogin.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           lblMsg.Text = Submit();
        }

        private string Submit()
        {
            var message = string.Empty;
            if (Page.IsValid)
            {
                try
                {
                    if (CommonFunctions.CheckIfUserExists(txtEmailId.Text, txtEmailId.Text, out message, "GU"))
                    {
                        using (var dataContext = new nChangerDb())
                        {
                            var id = txtEmailId.Text.ToLower();
                            var pass = txtPassword.Text.ToLower();

                            var user = dataContext.Users.FirstOrDefault(u => u.Email.ToLower().Equals(id) || u.UserId.ToLower().Equals(id));

                            if (user != null)
                            {
                                if (!user.Password.ToLower().Equals(pass))
                                {
                                    message = "Invalid Password";
                                     
                                }
                                else if (!user.EmailVerified.Value)
                                { 
                                    message = "Account Inactive. In case you just registered please confirm account using link emailed.";
                                   
                                }
                                else if (!user.IsActive)
                                {
                                    message = "Account Inactive.";
                                    
                                }
                                else
                                {
                                    Session.Add("USER_KEY", user.UserId);
                                    Session.Add("USR_NAME", user.FirstName + " " + user.LastName);
                                    AppBasePage appBase=new AppBasePage();
                                    appBase.Session.Add("USER_KEY", user.UserId);
                                    appBase.Session.Add("USR_NAME", user.FirstName + " " + user.LastName);
                                    appBase.Session.Add("USER_TYPE", user.UserTypeId);

                                    if(user.IsEligibilityConfirmed)
                                        Response.Redirect("~/Secured/dashboard.aspx");
                                    else
                                        Response.Redirect("~/Secured/ConfirmEligibility.aspx");
                                }
                            }
                        }
                    }
                    else
                    {
                        message = "Invalid E-mail address/ Username";
                    }

                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
            }
            return message;
        }
    }
}