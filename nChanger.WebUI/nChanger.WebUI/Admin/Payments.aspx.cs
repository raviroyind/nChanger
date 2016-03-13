using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Admin
{
    public partial class Payments : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "$('.date-input').datepicker();", true);
            if (!IsPostBack)
            {
                using (var dataContext=new nChangerDb())
                {
                    BindDropdownList(ddlPackage,dataContext.Packages.OrderBy(p => p.PackageName).ToList(),"Id", "PackageName");
                }
                BindGrid();
            }
        }

        private void BindGrid()
        {
            using (var dataContext=new nChangerDb())
            {
                var paymentList = dataContext.UserPayments.ToList();
                if (paymentList.Count > 0)
                {

                    if (!string.IsNullOrEmpty(txtUserId.Text))
                        paymentList = paymentList.Where(p => p.UserId.Equals(txtUserId.Text)).ToList();


                    if (ddlPackage.SelectedIndex != 0 && ddlPackage.SelectedIndex != -1)
                    {
                        var id = Guid.Parse(ddlPackage.SelectedValue);
                        paymentList = paymentList.Where(p => p.PackageId.Equals(id)).ToList();
                    }

                    if (ddlStatus.SelectedIndex != 0 && ddlStatus.SelectedIndex != -1)
                        paymentList = paymentList.Where(p => p.Status.Equals(ddlStatus.SelectedValue)).ToList();


                    if (!string.IsNullOrEmpty(txtImportDtFrom.Text))
                    {
                        var importDtFrom = Convert.ToDateTime(txtImportDtFrom.Text).Date;
                        paymentList = paymentList.Where(p => p.PaymentDate.Date >= importDtFrom).ToList();
                    }

                    if (!string.IsNullOrEmpty(txtImportDtTo.Text))
                    {
                        var importDtTo = Convert.ToDateTime(txtImportDtTo.Text).Date;
                        paymentList = paymentList.Where(p => p.PaymentDate.Date <= importDtTo).ToList();
                    } 

                    var dtSearch = CommonFunctions.ToDataTable<UserPayment>(paymentList);
                    if (dtSearch != null)
                    {
                        Session.Add("GetDataTable", dtSearch);
                        ucPaging.BindPaging(gvPayment, dtSearch, ucPaging.PageNo, "txt",
                            Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                        BindBottomPaging(ucPaging, ucPaging1);
                    }
                }
            }
        }

        #region Paging and Sorting

        protected
        void gvPayment_OnSorting(object sender, GridViewSortEventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPayment, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvPayment_Sorting(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPayment, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ddlNoOfRecords_IndexChanged(object sender, EventArgs e)
        {

            ucPaging.PageNo = "1";

            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvPayment, dt, ucPaging.PageNo, "First", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo_Changed(object sender, EventArgs e)
        {
            ucPaging.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];

            ucPaging.BindPaging(gvPayment, dt, ucPaging.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void ImgbtnNavigator_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvPayment, dt, ucPaging.PageNo, ucPaging.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo1_Changed(object sender, EventArgs e)
        {
            ucPaging1.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPayment, dt, ucPaging1.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ImgbtnNavigator1_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPayment, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }


        protected void gvPaymentImg_Sorting(object sender, EventArgs e)
        {

            ImageButton imgbtn = (ImageButton)sender;
            SetSorting(imgbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvPayment, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }



        #endregion Paging and Sorting

        protected void gvPayment_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    var lblPackageName = (Label)e.Row.FindControl("lblPackageName");
                    if (lblPackageName != null)
                    {
                        using (var dataContext = new nChangerDb())
                        {
                            var id = Guid.Parse(lblPackageName.Text);
                            lblPackageName.Text = dataContext.Packages.Find(id).PackageName;
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}