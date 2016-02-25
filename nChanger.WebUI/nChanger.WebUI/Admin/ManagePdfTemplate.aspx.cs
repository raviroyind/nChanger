using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using nChanger.Core;

namespace nChanger.WebUI.Admin
{
    public partial class ManagePdfTemplate : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Request.QueryString["id"] != null)
            //    hypBack.NavigateUrl = "ManageProvinceCategory.aspx?id=" + Request.QueryString["id"];

            var file = Request.Files["FileData"];

            if (file != null)
            {
                try
                {
                    var filePath = ConfigurationManager.AppSettings["FolderPath"] + file.FileName;


                    file.SaveAs(Server.MapPath(filePath));
                    UploadPdf(file.FileName);

                }
                catch (Exception exception)
                {
                    lblMsg.Text = exception.Message;
                }
            }



            this.Form.Enctype = "multipart / form - data";

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
            var id = Guid.Empty;

            if (Request.QueryString["id"] != null)
                id = Guid.Parse(Request.QueryString["id"]);
            else if (Request.UrlReferrer != null)
                id = Guid.Parse(Request.UrlReferrer.Query.Substring(4));


             using (var dataContext = new nChangerDb())
            {
                var templateList = dataContext.PdfFormTemplates.Where(p => p.ProvinceCategoryId.Equals(id)) .ToList();

                if (templateList.Count != 0)
                {
                    gvTemplate.Visible = true;
                    var dtSearch = CommonFunctions.ToDataTable<PdfFormTemplate>(templateList);

                    if (dtSearch != null)
                    {
                        Session.Add("GetDataTable", dtSearch);
                        ucPaging.BindPaging(gvTemplate, dtSearch, ucPaging.PageNo, "txt",
                            Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                        BindBottomPaging(ucPaging, ucPaging1);
                    }
                }
                else
                {
                    gvTemplate.Visible = false;
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

        private void UploadPdf(string file)
        {
            var id = Request.UrlReferrer.Query.Substring(4);
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    if (Request.UrlReferrer != null)
                        dataContext.PdfFormTemplates.Add(new PdfFormTemplate
                        {

                            Id = Guid.NewGuid(),
                            ProvinceCategoryId = Guid.Parse(Request.UrlReferrer.Query.Substring(4)),
                            PdfFileName = file,
                            TemplateName = file,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            EntryId = UserId,
                            EntryDate = DateTime.Now,
                            IsActive = false,
                        });

                    dataContext.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    lblMsg.Text += eve.Entry.Entity.GetType().Name + "<br/>" + eve.Entry.State;
                    foreach (var ve in eve.ValidationErrors)
                    {
                        lblMsg.Text += ve.PropertyName + "<br/>" + ve.ErrorMessage;
                    }
                }
            }

        }
        protected void updatePanelPdf_OnLoad(object sender, EventArgs e)
        {
            BindTemplates();
        }

        protected void gvTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = Guid.Parse(Convert.ToString(gvTemplate.DataKeys[e.RowIndex].Values[0]));

            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = dataContext.PdfFormTemplates.Find(id);
                    dataContext.PdfFormTemplates.Remove(dbEntry);
                    dataContext.SaveChanges();
                    lblMsg.Text = "Template " + dbEntry.TemplateName + " deleted successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);

                    BindTemplates();
                }
            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
            }
        }

        protected void gvTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvTemplate.EditIndex)
            {
                (e.Row.Cells[5].Controls[0] as ImageButton).Attributes["onclick"] =
                    "if(!confirm('Do you want to delete the record?')){ return false; };";
            }
        }
    }
}