using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.WebUI.Navigation;
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

        public string CurrentId
        {
            get { return (Session[AppConfig.CurrentId] == null ? "" : Session[AppConfig.CurrentId].ToString()); }
            set { Session[AppConfig.CurrentId] = value; }
        }

        public string RecordId
        {
            get { return (Session[AppConfig.RecordId] == null ? "" : Session[AppConfig.RecordId].ToString()); }
            set { Session[AppConfig.RecordId] = value; }
        }

        public int FormIndex
        {
            get
            {
                return (Session[AppConfig.CurrentFormIndex] == null ?-1 : Convert.ToInt32(Session[AppConfig.CurrentFormIndex].ToString()));
            }
            set { Session[AppConfig.CurrentFormIndex] = value; }
        }

        public List<FrmSection> Sections
        {
            get
            {
                return (Session[AppConfig.FormSetions] == null
                    ? null
                    : (List<FrmSection>) Session[AppConfig.FormSetions]);
            }
            set { Session[AppConfig.FormSetions] = value; }
            
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

            if (!string.IsNullOrEmpty(UserType))
            {
                if(Request.Url.AbsolutePath.Contains("Admin") && !UserType.Equals("AU"))
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

        public static List<string> GetCountriesList()
        {
            List<string> list = new List<string>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & CultureTypes.SpecificCultures);
            foreach (CultureInfo info in cultures)
            {
                RegionInfo info2 = new RegionInfo(info.LCID);
                if (!list.Contains(info2.EnglishName))
                {
                    list.Add(info2.EnglishName);
                }
            }

            list.Sort();
            return list;
        }

        public static List<string> GetYears()
        {
            List<string> list = new List<string>();
            for (int i = DateTime.Now.Year; i > 1899; i--)
            {
                list.Add(i.ToString());
            }

            return list;
        }


        public List<string> ExtractFromString(string text, string startString, string endString)
        {
            List<string> matched = new List<string>();
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            while (!exit)
            {
                indexStart = text.IndexOf(startString, StringComparison.Ordinal);
                indexEnd = text.IndexOf(endString, StringComparison.Ordinal);
                if (indexStart != -1 && indexEnd != -1)
                {
                    matched.Add(text.Substring(indexStart + startString.Length,
                        indexEnd - indexStart - startString.Length));
                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                    exit = true;
            }
            return matched;
        }

        #endregion Binding Functions...
    }
}
