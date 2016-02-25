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
    public partial class ManageQuestions : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindQuestions();
            }
        }

        private void BindQuestions()
        {
            using (var dataContext = new nChangerDb())
            {
                var provinceCategoryId = Guid.Parse(Request.QueryString["id"]);
                var questionsList = dataContext.DefineQuestions.Where(q => q.ProvinceCategoryId.Equals(provinceCategoryId)).ToList();

                if (questionsList.Count > 0)
                {
                    var dtSearch = CommonFunctions.ToDataTable<DefineQuestion>(questionsList);
                    if (dtSearch != null)
                    {
                        gvDefineQuestions.Visible = true;
                        Session.Add("GetDataTable", dtSearch);
                        ucPaging.BindPaging(gvDefineQuestions, dtSearch, ucPaging.PageNo, "txt",
                            Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                        BindBottomPaging(ucPaging, ucPaging1);
                    }
                }
                else
                    gvDefineQuestions.Visible = false;
            }
        }

        protected void btnAddQuestion_OnClick(object sender, EventArgs e)
        {
            Submit();
            hidQuestionId.Value = string.Empty;
            hidQuestion.Value = string.Empty;
            hidOptions.Value = string.Empty;
            BindQuestions();
        }

        public bool Submit()
        {
           
            var success = true;
            var id = string.IsNullOrEmpty(hidQuestionId.Value) ? Guid.NewGuid() : Guid.Parse(hidQuestionId.Value);
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var question = new DefineQuestion()
                    {
                        Id = id,
                        ProvinceCategoryId = Guid.Parse(Request.QueryString["id"]),
                        Question = hidQuestion.Value,
                        QuestionType = hidQuestionType.Value,
                       
                        IsActive =true,
                        EntryDate = DateTime.Now,
                        EntryIP = CommonFunctions.GetIpAddress(),
                        EntryId = UserId,

                    };

                    dataContext.DefineQuestions.AddOrUpdate(question);

                    #region Options...

                    dataContext.Database.ExecuteSqlCommand("DELETE FROM QuestionOptions WHERE DefineQuestionsId='" + id + "'");

                    if (!string.IsNullOrEmpty(hidOptions.Value))
                    {
                        var optionsArray = hidOptions.Value.Substring(0, hidOptions.Value.Length - 1).Split(',');

                        foreach (var option in optionsArray.Select(item => new QuestionOption
                        {
                            Id = Guid.NewGuid(),
                            DefineQuestionsId = id,
                            OptionLabel = item,
                            EntryDate = DateTime.Now,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            EntryId = UserId,
                            IsActive = true
                        }))
                        {
                            dataContext.QuestionOptions.AddOrUpdate(option);
                        }
                    }

                    #endregion Package Features...

                    dataContext.SaveChanges();
                    lblMsg.Text = "Question " + (string.IsNullOrEmpty(hidQuestionId.Value) ? "added" : "updated") + " successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                }
            }
            catch (DbEntityValidationException ex)
            {
                success = false;
            }

            return success;
        }


        #region Paging and Sorting

        protected void gvDefineQuestions_OnSorting(object sender, GridViewSortEventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvDefineQuestions, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvDefineQuestions_Sorting(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvDefineQuestions, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ddlNoOfRecords_IndexChanged(object sender, EventArgs e)
        {

            ucPaging.PageNo = "1";

            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvDefineQuestions, dt, ucPaging.PageNo, "First", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo_Changed(object sender, EventArgs e)
        {
            ucPaging.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];

            ucPaging.BindPaging(gvDefineQuestions, dt, ucPaging.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void ImgbtnNavigator_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvDefineQuestions, dt, ucPaging.PageNo, ucPaging.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo1_Changed(object sender, EventArgs e)
        {
            ucPaging1.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvDefineQuestions, dt, ucPaging1.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ImgbtnNavigator1_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvDefineQuestions, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }


        protected void gvDefineQuestionsImg_Sorting(object sender, EventArgs e)
        {

            ImageButton imgbtn = (ImageButton)sender;
            SetSorting(imgbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvDefineQuestions, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }



        #endregion Paging and Sorting


        protected void gvDefineQuestions_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvDefineQuestions.EditIndex)
            {
                (e.Row.Cells[2].Controls[0] as ImageButton).Attributes["onclick"] =
                    "if(!confirm('Do you want to delete the record?')){ return false; };";
            }
        }

        protected void gvDefineQuestions_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
             
        }

        protected void ddlProvince_OnSelectedIndexChanged(object sender, EventArgs e)
        {
             
        }
         
        protected void gvDefineQuestions_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}