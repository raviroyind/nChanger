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
    public partial class ManagePackageFeatures : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sortColumn"] = "EntryDate";
                ViewState["sortDirection"] = "ASC";

                //Start ################ Bellow code is for Initializing Paging ###############                
                TextBox txtPageno1 = (TextBox)ucPaging.FindControl("txtPageNo");

                txtPageno1.Attributes.Add("onkeypress", "return SetPagenoValue('" + txtPageno1.ClientID + "','" + txtPageno1.ClientID + "');");
                //End

                BindFeatures();
            }
        }

        private void BindFeatures()
        {
            using (var dataContext = new nChangerDb())
            {
                var featureList = dataContext.FeatureMasters.ToList();
                var dtSearch = CommonFunctions.ToDataTable<FeatureMaster>(featureList);

                if (dtSearch != null)
                {
                    Session.Add("GetDataTable", dtSearch);
                    ucPaging.BindPaging(gvFeature, dtSearch, ucPaging.PageNo, "txt",
                        Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                    BindBottomPaging(ucPaging, ucPaging1);
                }
            }
        }

        protected void btnAddFeature_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "trigg", "tinyMCE.get('" + txtDescription.UniqueID + "').save();", true);
            Submit();
            txtDescription.Text = string.Empty;
            hidFeatureName.Value = string.Empty;
            hidFeatureDesc.Value = string.Empty;
            hidFeatureId.Value = string.Empty;
            txtFeatureName.Text = string.Empty;
            BindFeatures();
             
        }

        public bool Submit()
        {
            var returnMessage = string.Empty;
            var success = true;
            var id = string.IsNullOrEmpty(hidFeatureId.Value) ? Guid.NewGuid() : Guid.Parse(hidFeatureId.Value);
            try
            {

                var message = string.Empty;

                using (var dataContext = new nChangerDb())
                {
                    var feature = new FeatureMaster()
                    {
                        Id = id,
                        Feature = hidFeatureName.Value,
                        Description = hidFeatureDesc.Value,
                        IsActive = true,
                        EntryDate = DateTime.Now,
                        EntryIP = CommonFunctions.GetIpAddress(),
                        EntryId = UserId
                    };

                    dataContext.FeatureMasters.AddOrUpdate(feature);
                    dataContext.SaveChanges();

                    lblMsg.Text = "Feature \"" + feature.Feature + "\" " + (string.IsNullOrEmpty(hidFeatureId.Value) ? "added" : "updated") + " successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                     
                }

            }
            catch (DbEntityValidationException ex)
            {
                success = false;
                returnMessage = ex.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors).Aggregate(returnMessage, (current, ve) => current + (ve.PropertyName + " Error Msg:" + ve.ErrorMessage));
            }
            
            return success;
        }

        protected void gvFeature_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                var id = Guid.Parse(Convert.ToString(gvFeature.DataKeys[e.NewEditIndex].Values[0]));

                using (var dataContext = new nChangerDb())
                {
                    var feature = dataContext.FeatureMasters.Find(id);
                    if (feature != null)
                    {
                        txtFeatureName.Text = feature.Feature;
                        txtDescription.Text = feature.Description;
                        hidFeatureId.Value = id.ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        #region Paging and Sorting

        protected void gvFeature_OnSorting(object sender, GridViewSortEventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFeature, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvFeature_Sorting(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFeature, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ddlNoOfRecords_IndexChanged(object sender, EventArgs e)
        {

            ucPaging.PageNo = "1";

            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvFeature, dt, ucPaging.PageNo, "First", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo_Changed(object sender, EventArgs e)
        {
            ucPaging.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];

            ucPaging.BindPaging(gvFeature, dt, ucPaging.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void ImgbtnNavigator_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvFeature, dt, ucPaging.PageNo, ucPaging.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo1_Changed(object sender, EventArgs e)
        {
            ucPaging1.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFeature, dt, ucPaging1.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ImgbtnNavigator1_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFeature, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }


        protected void gvFeatureImg_Sorting(object sender, EventArgs e)
        {

            ImageButton imgbtn = (ImageButton)sender;
            SetSorting(imgbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFeature, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }



        #endregion Paging and Sorting

        protected void gvFeature_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var id = Guid.Parse(Convert.ToString(gvFeature.DataKeys[gvFeature.SelectedIndex].Values[0]));

                using (var dataContext = new nChangerDb())
                {
                    var feature = dataContext.FeatureMasters.Find(id);
                    if (feature != null)
                    {
                        txtFeatureName.Text = feature.Feature;
                        txtDescription.Text = feature.Description;
                        hidFeatureId.Value = id.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "loadEdit()", true);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
         
        protected void gvFeature_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = Guid.Parse(Convert.ToString(gvFeature.DataKeys[e.RowIndex].Values[0]));

            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = dataContext.FeatureMasters.Find(id);
                    dataContext.FeatureMasters.Remove(dbEntry);
                    dataContext.SaveChanges();
                    lblMsg.Text = "Feature " + dbEntry.Feature + " deleted successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                    BindFeatures();
                }
            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
            }
        }

        protected void gvFeature_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvFeature.EditIndex)
            {
                (e.Row.Cells[2].Controls[0] as ImageButton).Attributes["onclick"] =
                    "if(!confirm('Do you want to delete the record?')){ return false; };";
            }
        }
    }
}