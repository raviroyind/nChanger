using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Admin
{
    public partial class ManageProvinces : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                BindProvince();
            }

        }

        private void BindProvince()
        {
            using (var dataContext = new nChangerDb())
            {
                var provinceList = dataContext.Provinces.ToList();
                var dtSearch = CommonFunctions.ToDataTable<Province>(provinceList);

                if (dtSearch != null)
                {
                    Session.Add("GetDataTable", dtSearch);
                    ucPaging.BindPaging(gvProvince, dtSearch, ucPaging.PageNo, "txt",
                        Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                    BindBottomPaging(ucPaging, ucPaging1);
                }
            }
        }

        protected void btnAddProvince_Click(object sender, EventArgs e)
        {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "trigg", "tinyMCE.get('" + txtProvinceDescription.UniqueID + "').save();",true);
            if (Submit())
            {
                txtProvinceDescription.Text = string.Empty;
                hidName.Value = string.Empty;
                hidDesc.Value = string.Empty;
                hiddenProvinceId.Value = string.Empty;
                lblMsg.Text = "Province " + txtProvince.Text + " added successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                txtProvince.Text = string.Empty;
                BindProvince();
            }

            
        }

        public bool Submit()
        {
            var returnMessage = string.Empty;
            var success = true;
            var id = string.IsNullOrEmpty(hiddenProvinceId.Value) ? Guid.NewGuid() : Guid.Parse(hiddenProvinceId.Value);
            try
            {

                var message = string.Empty;

                using (var dataContext = new nChangerDb())
                {
                    var province = new Province()
                    {
                        Id = id,
                        ProvinceName = hidName.Value,
                        Description = hidDesc.Value,
                        IsActive = true,
                        EntryDate = DateTime.Now,
                        EntryIP = CommonFunctions.GetIpAddress(),
                        EntryId = UserId
                    };

                    dataContext.Provinces.AddOrUpdate(province);
                    dataContext.SaveChanges();
                }

            }
            catch (DbEntityValidationException ex)
            {
                success = false;
                returnMessage = ex.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors).Aggregate(returnMessage, (current, ve) => current + (ve.PropertyName + " Error Msg:" + ve.ErrorMessage));
            }

           // lblMsg.Text = returnMessage;
            return success;
        }

        protected void gvProvince_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                var id = Guid.Parse(Convert.ToString(gvProvince.DataKeys[e.NewEditIndex].Values[0]));

                using (var dataContext = new nChangerDb())
                {
                    var province = dataContext.Provinces.Find(id);
                    if (province != null)
                    {
                        txtProvince.Text = province.ProvinceName;
                        txtProvinceDescription.Text = province.Description;
                        hiddenProvinceId.Value = id.ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }


        #region Paging and Sorting

        protected void gvProvince_OnSorting(object sender, GridViewSortEventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvince, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvProvince_Sorting(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvince, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ddlNoOfRecords_IndexChanged(object sender, EventArgs e)
        {

            ucPaging.PageNo = "1";

            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvProvince, dt, ucPaging.PageNo, "First", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo_Changed(object sender, EventArgs e)
        {
            ucPaging.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];

            ucPaging.BindPaging(gvProvince, dt, ucPaging.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void ImgbtnNavigator_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvProvince, dt, ucPaging.PageNo, ucPaging.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo1_Changed(object sender, EventArgs e)
        {
            ucPaging1.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvince, dt, ucPaging1.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ImgbtnNavigator1_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvince, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }


        protected void gvProvinceImg_Sorting(object sender, EventArgs e)
        {

            ImageButton imgbtn = (ImageButton)sender;
            SetSorting(imgbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvProvince, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }



        #endregion Paging and Sorting

        protected void gvProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var id = Guid.Parse(Convert.ToString(gvProvince.DataKeys[gvProvince.SelectedIndex].Values[0]));

                using (var dataContext = new nChangerDb())
                {
                    var province = dataContext.Provinces.Find(id);
                    if (province != null)
                    {
                        txtProvince.Text = province.ProvinceName;
                        txtProvinceDescription.Text = province.Description;
                        hiddenProvinceId.Value = id.ToString();
                        ScriptManager.RegisterStartupScript(this,this.GetType(), Guid.NewGuid().ToString() , "loadEdit()", true);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected void UpdatePanel1_OnLoad(object sender, EventArgs e)
        {

        }

        protected void gvProvince_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = Guid.Parse(Convert.ToString(gvProvince.DataKeys[e.RowIndex].Values[0]));

            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = dataContext.Provinces.Find(id);
                    dataContext.Provinces.Remove(dbEntry);
                    dataContext.SaveChanges();
                    lblMsg.Text = "Province " + dbEntry.ProvinceName + " deleted successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                    
                     BindProvince();
                }
            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
            }
           
        }

        protected void gvProvince_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvProvince.EditIndex)
            {
                (e.Row.Cells[2].Controls[0] as ImageButton).Attributes["onclick"] =
                    "if(!confirm('Do you want to delete the record?')){ return false; };";
            }
        }

        protected void lnkViewCategories_OnClick(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;

            if (lnk.CommandName.Equals("View"))
                LoadCategories(Guid.Parse(lnk.CommandArgument));
        }


        private void LoadCategories(Guid id)
        {
            using (var dataContext = new nChangerDb())
            {
                var categories = dataContext.Provinces.Find(id).ProvinceCategories.ToList();
                if(categories.Count>0)
                    BindCategories(categories);
            }
        }


        private void BindCategories(List<ProvinceCategory> list)
        {
            lblCategories.Text = "Province " + list[0].Province.ProvinceName + " Categories list.";
            gvCategories.DataSource = list;
            gvCategories.DataBind();
        }
    }
}