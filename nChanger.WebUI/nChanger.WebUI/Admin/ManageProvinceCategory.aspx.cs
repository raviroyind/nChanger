using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Admin
{
    public partial class ManageProvinceCategory : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdown();
                 
                if (Request.QueryString["id"] != null)
                    BindCateory(Guid.Parse(Request.QueryString["id"]));
                else
                    BindCateory();
            }
        }

        private void BindDropdown()
        {
            using (var dataContext = new nChangerDb())
            {
                BindDropdownList(ddlProvince,dataContext.Provinces.ToList(),"Id", "ProvinceName");
                BindDropdownList(ddlProvinceAdd, dataContext.Provinces.ToList(), "Id", "ProvinceName");
                if (Request.QueryString["id"] != null)
                {
                    ddlProvinceAdd.ClearSelection();
                    ddlProvinceAdd.Items.FindByValue(Request.QueryString["id"]).Selected = true;
                    ddlProvinceAdd.CssClass = "ui normal selection dropdown disabled";
                }
                else
                    ddlProvinceAdd.CssClass = "ui normal selection dropdown";
            }
        }

        private void BindCateory(Guid provinceId = default(Guid))
        {
            var guidIsEmpty = provinceId == Guid.Empty;

            using (var dataContext = new nChangerDb())
            {
                 var categoryList = dataContext.ProvinceCategories.ToList();

                if (!guidIsEmpty)
                {
                    categoryList = categoryList.Where(p => p.ProvinceId.Equals(provinceId)).ToList();

                    if(Request.QueryString["id"]!=null)
                        ddlProvince.CssClass = "ui normal selection dropdown disabled";

                    lblCurrentProvince.Text = dataContext.Provinces.Find(provinceId).ProvinceName;
                    ddlProvince.Items.FindByValue(provinceId.ToString()).Selected = true;
                    lblCaption.InnerHtml = lblCurrentProvince.Text + " >> Category List.";
                }
                else
                    lblCaption.InnerHtml = lblCurrentProvince.Text + "Category List.";

                if (categoryList.Count > 0)
                { 
                    var dtSearch = CommonFunctions.ToDataTable<ProvinceCategory>(categoryList);
                    if (dtSearch != null)
                    {
                        Session.Add("GetDataTable", dtSearch);
                        ucPaging.BindPaging(gvProvinceCategory, dtSearch, ucPaging.PageNo, "txt",
                            Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                        BindBottomPaging(ucPaging, ucPaging1);
                    }
                }
            }
        }

        protected void btnAddProvinceCateory_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "trigg", "tinyMCE.get('" + txtProvinceCateoryDescription.UniqueID + "').save();", true);
             
            if (Submit())
            {
                txtProvinceCateoryDescription.Text = string.Empty;
                hidCategoryName.Value = string.Empty;
                hidCategoryDesc.Value = string.Empty;
                hiddenCategoryId.Value = string.Empty;
                hiddenProvinceId.Value = string.Empty;
                  
                lblMsg.Text = "Category " + txtCateory.Text + " added successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                txtCateory.Text = string.Empty;
                if (Request.QueryString["id"] != null)
                    BindCateory(Guid.Parse(Request.QueryString["id"]));
                else
                    BindCateory();
            }
        }


        public bool Submit()
        {
            var success = true;
            var id = string.IsNullOrEmpty(hiddenCategoryId.Value) ? Guid.NewGuid() : Guid.Parse(hiddenCategoryId.Value);
            try
            { 
                using (var dataContext = new nChangerDb())
                {
                    var category = new ProvinceCategory()
                    {
                        Id = id,
                        ProvinceId = string.IsNullOrEmpty(hiddenProvinceId.Value)? Guid.Parse(ddlProvinceAdd.SelectedValue):Guid.Parse(hiddenProvinceId.Value),
                        Category = hidCategoryName.Value,
                        Description = hidCategoryDesc.Value,
                        IsActive = true,
                        EntryDate = DateTime.Now,
                        EntryIP = CommonFunctions.GetIpAddress(),
                        EntryId = UserId
                    };

                    dataContext.ProvinceCategories.AddOrUpdate(category);
                    dataContext.SaveChanges();
                }

            }
            catch (DbEntityValidationException ex)
            {
                success = false;
            }
             
            return success;
        }


        #region Paging and Sorting

        protected void gvProvinceCategory_OnSorting(object sender, GridViewSortEventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvinceCategory, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvProvinceCategory_Sorting(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvinceCategory, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ddlNoOfRecords_IndexChanged(object sender, EventArgs e)
        {

            ucPaging.PageNo = "1";

            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvProvinceCategory, dt, ucPaging.PageNo, "First", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo_Changed(object sender, EventArgs e)
        {
            ucPaging.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];

            ucPaging.BindPaging(gvProvinceCategory, dt, ucPaging.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void ImgbtnNavigator_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvProvinceCategory, dt, ucPaging.PageNo, ucPaging.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo1_Changed(object sender, EventArgs e)
        {
            ucPaging1.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvinceCategory, dt, ucPaging1.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ImgbtnNavigator1_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvinceCategory, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }


        protected void gvProvinceCategoryImg_Sorting(object sender, EventArgs e)
        {

            ImageButton imgbtn = (ImageButton)sender;
            SetSorting(imgbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvinceCategory, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }



        #endregion Paging and Sorting

        
        protected void gvProvinceCategory_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvProvinceCategory.EditIndex)
            {
                (e.Row.Cells[2].Controls[0] as ImageButton).Attributes["onclick"] =
                    "if(!confirm('Do you want to delete the record?')){ return false; };";
            }
        }

        protected void gvProvinceCategory_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = Guid.Parse(Convert.ToString(gvProvinceCategory.DataKeys[e.RowIndex].Values[0]));

            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = dataContext.ProvinceCategories.Find(id);
                    dataContext.ProvinceCategories.Remove(dbEntry);
                    dataContext.SaveChanges();
                    lblMsg.Text = "Category " + dbEntry.Category + " deleted successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                    if (Request.QueryString["id"] != null)
                        BindCateory(Guid.Parse(Request.QueryString["id"]));
                    else
                        BindCateory();
                }
            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
            }
        }
         
        protected void ddlProvince_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlProvince.SelectedIndex==0)
                BindCateory();
            else
                BindCateory(Guid.Parse(ddlProvince.SelectedValue));
        }
         
        protected void gvProvinceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var id = Guid.Parse(Convert.ToString(gvProvinceCategory.DataKeys[gvProvinceCategory.SelectedIndex].Values[0]));

                using (var dataContext = new nChangerDb())
                {
                    var category = dataContext.ProvinceCategories.Find(id);
                    if (category != null)
                    {
                        ddlProvinceAdd.ClearSelection();
                        ddlProvinceAdd.Items.FindByValue(category.ProvinceId.ToString()).Selected = true;
                        txtCateory.Text = category.Category;
                        txtProvinceCateoryDescription.Text = category.Description;
                        hiddenCategoryId.Value = id.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "loadEdit()", true);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}