using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace nChanger.WebUI.UserControls
{
    public partial class _topNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
             
            if (Session["USER_KEY"] != null)
            {
                if (Convert.ToString(Session["USER_TYPE"]) == "AU")
                {
                    hypLogOut.NavigateUrl = "../adminlogin.aspx?id=lg";
                }
                else
                    hypProfile.NavigateUrl = "../Secured/Profile.aspx?id="+Convert.ToString(Session["USER_KEY"]);


                var anc = (HtmlAnchor)this.Page.Master.FindControl("ancLogin");
                anc.Visible = false;
                hypUserInfo.Text = "<i class=\"user icon\"></i> Welcome " + Convert.ToString(Session["USR_NAME"]);
            }
            else
            {
                login.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
        }
    }
}