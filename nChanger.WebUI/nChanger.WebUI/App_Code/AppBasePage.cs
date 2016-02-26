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

        public string PreviousPageId
        {
            get
            {
                return (Session[AppConfig.PreviousPageId] == null ? "" : Session[AppConfig.PreviousPageId].ToString());
            }
            set { Session[AppConfig.PreviousPageId] = value; }
        }
        

        protected override void OnLoad(EventArgs e)
        {
            if (!Request.Url.AbsolutePath.Contains("index.aspx") || !Request.Url.AbsolutePath.Contains("adminlogin.aspx"))
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    if (!Request.Url.AbsolutePath.ToLower().Contains("admin"))
                        Response.Redirect("../Index.aspx?id=ua");
                    else
                        Response.Redirect("../adminlogin.aspx?id=tm");
                }
                     
            }

            var nRet = Session["UnAuthorizedAccess"] ?? -1;
            if (nRet.ToString() == "-1")
            {
                if (!Request.Url.AbsolutePath.Contains("Admin"))
                    Response.Redirect("../Index.aspx?id=ua");
                else
                    Response.Redirect("../adminlogin.aspx?id=tm");
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


        protected void ShowUpdatePanelAlert(UpdatePanel updatePanel,string message)
        {
            ScriptManager.RegisterStartupScript(
                updatePanel,
                updatePanel.GetType(),
                Guid.NewGuid().ToString(),
                " alert('"+ message + "');",
                true);
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
