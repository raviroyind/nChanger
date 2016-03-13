using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Admin
{
    public partial class ManagePackage : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPackages();
                BindFeaturesList();
            }
        }

        private void BindPackages()
        {
            using (var dataContext = new nChangerDb())
            {
                var packageList = dataContext.Packages.ToList();

                if (packageList.Count > 0)
                {
                    var dtSearch = CommonFunctions.ToDataTable<Package>(packageList);
                    if (dtSearch != null)
                    {
                        gvPackage.Visible = true;
                        Session.Add("GetDataTable", dtSearch);
                        ucPaging.BindPaging(gvPackage, dtSearch, ucPaging.PageNo, "txt",
                            Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                        BindBottomPaging(ucPaging, ucPaging1);
                    }
                }
                else
                    gvPackage.Visible = false;
            }
        }

        private void BindFeaturesList()
        {
            using (var dataContext = new nChangerDb())
            {
                var featureList = dataContext.FeatureMasters.Where(f => f.IsActive).OrderBy(f => f.Feature).ToList();

                if (featureList.Count > 0)
                {
                    chkFeatures.DataSource = featureList;
                    chkFeatures.DataTextField = "Feature";
                    chkFeatures.DataValueField = "Id";
                    chkFeatures.DataBind();
                }
            }
        }

        #region Paging and Sorting

            protected
            void gvPackage_OnSorting(object sender, GridViewSortEventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPackage, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvPackage_Sorting(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPackage, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ddlNoOfRecords_IndexChanged(object sender, EventArgs e)
        {

            ucPaging.PageNo = "1";

            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvPackage, dt, ucPaging.PageNo, "First", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo_Changed(object sender, EventArgs e)
        {
            ucPaging.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];

            ucPaging.BindPaging(gvPackage, dt, ucPaging.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void ImgbtnNavigator_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvPackage, dt, ucPaging.PageNo, ucPaging.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo1_Changed(object sender, EventArgs e)
        {
            ucPaging1.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPackage, dt, ucPaging1.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ImgbtnNavigator1_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPackage, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }


        protected void gvPackageImg_Sorting(object sender, EventArgs e)
        {

            ImageButton imgbtn = (ImageButton)sender;
            SetSorting(imgbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPackage, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }



        #endregion Paging and Sorting

        protected void gvPackage_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvPackage.EditIndex)
            {
                (e.Row.Cells[4].Controls[0] as ImageButton).Attributes["onclick"] =
                    "if(!confirm('Do you want to delete the record?')){ return false; };";
            }
        }

        protected void gvPackage_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = Guid.Parse(Convert.ToString(gvPackage.DataKeys[e.RowIndex].Values[0]));

            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = dataContext.Packages.Find(id);
                    dataContext.Packages.Remove(dbEntry);
                    dataContext.SaveChanges();
                    BindPackages();
                    lblMsg.Text = "Package \"" + dbEntry.PackageName + "\" deleted successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                }
            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
            }
        }
 
        protected void gvPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var id = Guid.Parse(Convert.ToString(gvPackage.DataKeys[gvPackage.SelectedIndex].Values[0]));

                using (var dataContext = new nChangerDb())
                {
                    var package = dataContext.Packages.Find(id);
                    if (package != null)
                    {
                        txtPackageName.Text = package.PackageName;
                        txtPrice.Text = package.Price.ToString("N");
                        chkIsActive.Checked = package.IsActive;
                        hidPackageId.Value = id.ToString();


                        if (package.PackageFeatures.Count > 0)
                        {
                            foreach (var feature in package.PackageFeatures)
                            {
                                chkFeatures.Items.FindByValue(feature.FeatureId.ToString()).Selected = true;
                            }
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "loadEdit()", true);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected void btnAddPackage_OnClick(object sender, EventArgs e)
        {
            Submit();
            hidPackageId.Value = string.Empty;
            hidPackageName.Value = string.Empty;
            hidPrice.Value = string.Empty;
            hidFeatures.Value = string.Empty;
            chkFeatures.ClearSelection();
            hidActive.Value = string.Empty;
            chkIsActive.Checked = true;
            BindPackages();
        }

        public bool Submit()
        {
            var success = true;
            var id = string.IsNullOrEmpty(hidPackageId.Value) ? Guid.NewGuid() : Guid.Parse(hidPackageId.Value);
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var package = new Package()
                    {
                        Id = id,
                        PackageName = hidPackageName.Value,
                        Price = Convert.ToDecimal(hidPrice.Value),
                        IsActive = !string.IsNullOrEmpty(hidActive.Value) && Convert.ToBoolean(hidActive.Value),
                        EntryDate = DateTime.Now,
                        EntryIP = CommonFunctions.GetIpAddress(),
                        EntryId = UserId,

                    };

                    dataContext.Packages.AddOrUpdate(package);

                    #region Package Features...
                     
                    dataContext.Database.ExecuteSqlCommand("DELETE FROM PackageFeature WHERE PackageId='"+id+"'");

                    if (!string.IsNullOrEmpty(hidFeatures.Value))
                    {
                        var featureArray = hidFeatures.Value.Substring(0, hidFeatures.Value.Length - 1).Split(',');

                        foreach (var feature in featureArray.Select(item => new PackageFeature
                        {
                            Id = Guid.NewGuid(),
                            PackageId = id,
                            FeatureId = Guid.Parse(chkFeatures.Items.FindByText(item).Value),
                            FeatureName = item,
                            EntryDate = DateTime.Now,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            EntryId = UserId,
                            IsActive = true
                        }))
                        {
                            dataContext.PackageFeatures.AddOrUpdate(feature);
                        }
                    }

                    #endregion Package Features...
                     
                    dataContext.SaveChanges();
                    lblMsg.Text = "Package \"" + package.PackageName + "\" " + (string.IsNullOrEmpty(hidPackageId.Value) ?  "added" : "updated") + " successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                }
            }
            catch (DbEntityValidationException ex)
            {
                success = false;
            }

            return success;
        }

        //protected void updatePanelPdf_OnLoad(object sender, EventArgs e)
        //{
        //    var src = sender;
            
        //    BindPackages();
        //}
    }
}