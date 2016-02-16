using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using nChanger.Core;
using System.IO;

namespace nChanger.WebUI.Admin
{
    public partial class PdfUpload : AppBasePage
    {
        #region Variable Declarations...

        readonly nChangerDb _dataContext = new nChangerDb();
        readonly List<FieldMapping> _fieldMapping = new List<FieldMapping>();
        private IList tableList;
        private string _fileName;
        private IQueryable<string> _columns;

        #endregion Variable Declarations...

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Enctype= "multipart / form - data";

            if (!IsPostBack)
            {

                ViewState["sortColumn"] = "EntryDate";
                ViewState["sortDirection"] = "DESC";

                //Start ################ Bellow code is for Initializing Paging ###############                
                TextBox txtPageno1 = (TextBox)ucPaging.FindControl("txtPageNo");

                txtPageno1.Attributes.Add("onkeypress", "return SetPagenoValue('" + txtPageno1.ClientID + "','" + txtPageno1.ClientID + "');");
                //End
                BindTemplates();
            }
        }

        private void BindTemplates()
        {
            using (var dataContext = new nChangerDb())
            {
                var templateList = dataContext.PdfTemplates.ToList();

                if (templateList.Count != 0)
                {
                    var dtSearch = CommonFunctions.ToDataTable<PdfTemplate>(templateList);

                    if (dtSearch != null)
                    {
                        Session.Add("GetDataTable", dtSearch);
                        ucPaging.BindPaging(gvTemplate, dtSearch, ucPaging.PageNo, "txt",
                            Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                        BindBottomPaging(ucPaging, ucPaging1);
                    }
                }
            }
        }

        protected void gvTemplate_OnSorting(object sender, GridViewSortEventArgs e)
        {
          
        }
         
        #region Paging and Sorting

        protected void ddlNoOfRecords_IndexChanged(object sender, EventArgs e)
        {
            ucPaging.PageNo = "1";
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvTemplate, dt, ucPaging.PageNo, "First", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo_Changed(object sender, EventArgs e)
        {
            ucPaging.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];

            ucPaging.BindPaging(gvTemplate, dt, ucPaging.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void ImgbtnNavigator_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvTemplate, dt, ucPaging.PageNo, ucPaging.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo1_Changed(object sender, EventArgs e)
        {
            ucPaging1.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvTemplate, dt, ucPaging1.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ImgbtnNavigator1_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvTemplate, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvTemplate_Sorting(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvTemplate, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvTemplateImg_Sorting(object sender, EventArgs e)
        {

            ImageButton imgbtn = (ImageButton)sender;
            SetSorting(imgbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvTemplate, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        #endregion Paging and Sorting

        protected void asynpdfUpload_OnUploadComplete(object sender, AjaxFileUploadEventArgs e)
        {
            try
            {
                var fileName = Path.GetFileName(e.FileName);
                asynpdfUpload.SaveAs(Server.MapPath("../Pdf/" + fileName));

                using (var dataContext=new nChangerDb())
                {
                    dataContext.PdfTemplates.Add(new PdfTemplate
                    {
                        Id = Guid.NewGuid(),
                        PdfFileName = Path.GetFileName(e.FileName),
                        TemplateName = Path.GetFileNameWithoutExtension(e.FileName),
                        EntryIP=CommonFunctions.GetIpAddress(),
                        EntryId = UserId,
                        EntryDate = DateTime.Now,
                        IsActive=false,
                    });

                    dataContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
            }
            
        }

        protected void asynpdfUpload_OnUploadCompleteAll(object sender, AjaxFileUploadCompleteAllEventArgs e)
        {
            ViewState["sortColumn"] = "EntryDate";
            ViewState["sortDirection"] = "DESC";

            //Start ################ Bellow code is for Initializing Paging ###############                
            TextBox txtPageno1 = (TextBox)ucPaging.FindControl("txtPageNo");

            txtPageno1.Attributes.Add("onkeypress", "return SetPagenoValue('" + txtPageno1.ClientID + "','" + txtPageno1.ClientID + "');");
            //End
            BindTemplates();
        }
    }
}