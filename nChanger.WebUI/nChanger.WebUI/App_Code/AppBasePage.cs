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

        public string UserType
        {
            get
            {
                return (Session[AppConfig.SessionItemNameUserType] == null ? "" : Session[AppConfig.SessionItemNameUserType].ToString());
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

        protected void BindBottomPaging(UserControl ucPaging, UserControl ucPaging1)
        {
            (ucPaging1.FindControl("txtPageNo") as TextBox).Text = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            (ucPaging1.FindControl("lblTotPages") as Label).Text = (ucPaging.FindControl("lblTotPages") as Label).Text;
            (ucPaging1.FindControl("lnkimgbtnFirst") as LinkButton).Enabled = (ucPaging.FindControl("lnkimgbtnFirst") as LinkButton).Enabled;
            (ucPaging1.FindControl("lnkimgbtnPrevious") as LinkButton).Enabled = (ucPaging.FindControl("lnkimgbtnPrevious") as LinkButton).Enabled;
            (ucPaging1.FindControl("lnkimgbtnNext") as LinkButton).Enabled = (ucPaging.FindControl("lnkimgbtnNext") as LinkButton).Enabled;
            (ucPaging1.FindControl("lnkimgbtnLast") as LinkButton).Enabled = (ucPaging.FindControl("lnkimgbtnLast") as LinkButton).Enabled;
        }

        protected void SetSorting(string sSortExp)
        {
            if (Convert.ToString(ViewState["sortColumn"]) == sSortExp)
            {
                if (ViewState["sortDirection"] != null)
                {
                    if ("ASC" == ViewState["sortDirection"].ToString())
                        ViewState["sortDirection"] = "DESC";
                    else
                        ViewState["sortDirection"] = "ASC";
                }
                else
                    ViewState["sortDirection"] = "ASC";
            }
            else
            {
                ViewState["sortColumn"] = sSortExp;
                ViewState["sortDirection"] = "ASC";
            }

        }

        #endregion Binding Functions...
    }
}
