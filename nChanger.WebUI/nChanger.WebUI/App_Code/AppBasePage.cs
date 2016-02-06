using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nChanger.WebUI
{
    public class AppBasePage : Page
    { 
        public string UserId
        {
            get
            {
                return (Session[AppConfig.SessionItemNameUserId]==null? "" : Session[AppConfig.SessionItemNameUserId].ToString());
            }
             
        }

        public string UserName
        {
            get
            {
                return (Session[AppConfig.SessionItemNameUserName] == null ? "" : Session[AppConfig.SessionItemNameUserName].ToString());
            }
            
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Request.Url.AbsolutePath.Contains("index.aspx"))
            {
                if (string.IsNullOrEmpty(UserId))
                    Response.Redirect("../index.aspx?id=ua", true);
            }

            var nRet = Session["UnAuthorizedAccess"] ?? -1;
            if (nRet.ToString() == "-1")
            {
                Response.Redirect("../Index.aspx?id=ua");
            }
            base.OnLoad(e);

        }

        #region Binding Functions...

        protected void BindDropdownList(DropDownList ddl, object src, string valueField, string textField)
        {

            ddl.DataSource = src;
            
            if(!string.IsNullOrEmpty(textField))
                ddl.DataTextField = textField;

            if (!string.IsNullOrEmpty(valueField))
             ddl.DataValueField = valueField;

            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--SELECT--", "SEL"));
        }

        #endregion Binding Functions...
    }
}
