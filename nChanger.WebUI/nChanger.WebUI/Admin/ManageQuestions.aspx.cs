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
                BindDropdown();

                if (Request.QueryString["id"] != null)
                    BindQuestions(Guid.Parse(Request.QueryString["id"]));
                else
                    BindQuestions();


            }
        }

        private void BindDropdown()
        {
            using (var dataContext = new nChangerDb())
            {
                BindDropdownList(ddlCategory, dataContext.ProvinceCategories.ToList(), "Id", "Category");
                BindDropdownList(ddlCategoryAdd, dataContext.ProvinceCategories.ToList(), "Id", "Category");

                if (Request.QueryString["id"] != null)
                {
                    ddlCategoryAdd.ClearSelection();
                    ddlCategoryAdd.Items.FindByValue(Request.QueryString["id"]).Selected = true;
                    ddlCategoryAdd.CssClass = "ui normal selection dropdown disabled";
                }
                else
                    ddlCategoryAdd.CssClass = "ui normal selection dropdown";
            }
        }

        private void BindQuestions(Guid categoryId = default(Guid))
        {
            var guidIsEmpty = categoryId == Guid.Empty;
            using (var dataContext = new nChangerDb())
            {
                var questionsList = dataContext.DefineQuestions.ToList();
                if (!guidIsEmpty)
                {
                    questionsList = dataContext.DefineQuestions.Where(q => q.ProvinceCategoryId.Equals(categoryId)).ToList();

                    if (Request.QueryString["id"] != null)
                    {
                        ddlCategory.CssClass = "ui normal selection dropdown disabled";
                        ddlCategory.Items.FindByValue(categoryId.ToString()).Selected = true;
                    }
                }
                
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
                        ProvinceCategoryId = string.IsNullOrEmpty(hidProvinceCategoryId.Value)?Guid.Parse(ddlCategoryAdd.SelectedValue):Guid.Parse(hidProvinceCategoryId.Value),
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
                        if (!hidOptions.Value.Contains(","))
                        {
                            var option = new QuestionOption
                            {
                                Id = Guid.NewGuid(),
                                DefineQuestionsId = id,
                                OptionLabel = hidOptions.Value,
                                EntryDate = DateTime.Now,
                                EntryIP = CommonFunctions.GetIpAddress(),
                                EntryId = UserId,
                                IsActive = true
                            };

                            dataContext.QuestionOptions.AddOrUpdate(option);

                        }
                        else
                        {
                            var optionsArray = hidOptions.Value.EndsWith(",")? hidOptions.Value.Substring(0, hidOptions.Value.Length - 1).Split(','): hidOptions.Value.Split(',');

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
                    }

                    #endregion Package Features...

                    dataContext.SaveChanges();
                    lblMsg.Text = "Question " + (string.IsNullOrEmpty(hidQuestionId.Value) ? "added" : "updated") + " successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                }
            }
            catch (DbEntityValidationException ex)
            {
                lblMsg.Text = ex.Message;
                success = false;
            }

            return success;
        }

        protected void gvDefineQuestions_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = Guid.Parse(Convert.ToString(gvDefineQuestions.DataKeys[e.RowIndex].Values[0]));

            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = dataContext.DefineQuestions.Find(id);
                    dataContext.DefineQuestions.Remove(dbEntry);
                    dataContext.SaveChanges();
                    lblMsg.Text = "Question \"" + dbEntry.Question + "\" deleted successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                    if (Request.QueryString["id"] != null)
                        BindQuestions(Guid.Parse(Request.QueryString["id"]));
                    else
                        BindQuestions();
                }
            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
            }
        }

        protected void gvDefineQuestions_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var id = Guid.Parse(Convert.ToString(gvDefineQuestions.DataKeys[gvDefineQuestions.SelectedIndex].Values[0]));

                using (var dataContext = new nChangerDb())
                {
                    var question = dataContext.DefineQuestions.Find(id);
                    if (question != null)
                    {
                        txtQuestion.Text = question.Question;
                        ddlQuestionTypeAdd.ClearSelection();
                        ddlQuestionTypeAdd.Items.FindByValue(question.QuestionType).Selected = true;
                        hidQuestionId.Value = id.ToString();

                        if (Request.QueryString["id"] != null)
                        {
                            ddlCategoryAdd.ClearSelection();
                            ddlCategoryAdd.Items.FindByValue(Request.QueryString["id"]).Selected = true;
                            ddlCategoryAdd.CssClass = "ui normal selection dropdown disabled";
                        }
                        else
                            ddlCategoryAdd.CssClass = "ui normal selection dropdown";

                        if (question.QuestionOptions.Count > 0)
                        {
                            txtOptionLabel.Text = string.Empty;
                            foreach (var option in question.QuestionOptions)
                            {
                                txtOptionLabel.Text += option.OptionLabel + ",";
                            }

                            if (txtOptionLabel.Text.EndsWith(","))
                                txtOptionLabel.Text = txtOptionLabel.Text.Substring(0, txtOptionLabel.Text.Length - 1);
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

        protected void gvDefineQuestions_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvDefineQuestions.EditIndex)
            {
                (e.Row.Cells[2].Controls[0] as ImageButton).Attributes["onclick"] =
                    "if(!confirm('Do you want to delete the record?')){ return false; };";

                var lblType = (Label) e.Row.FindControl("lblType");
                if (lblType == null) return;
                switch (lblType.Text)
                {
                    case "txt":
                        lblType.Text = "Input box";
                        break;
                    case "tar":
                        lblType.Text = "Text area";
                        break;
                    case "chk":
                        lblType.Text = "Check box";
                        break;
                    case "rdb":
                        lblType.Text = "Radio button";
                        break;
                }
            }
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

        protected void btnAddQuestion_OnClick(object sender, EventArgs e)
        {
            Submit();
            hidQuestionId.Value = string.Empty;
            hidProvinceCategoryId.Value = string.Empty;
            hidQuestion.Value = string.Empty;
            hidOptions.Value = string.Empty;
            hidQuestionType.Value = string.Empty;
            txtQuestion.Text = string.Empty;
            txtOptionLabel.Text = string.Empty;
           
            if (Request.QueryString["id"] != null)
                BindQuestions(Guid.Parse(Request.QueryString["id"]));
            else
                BindQuestions();
        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex == 0)
                BindQuestions();
            else
                BindQuestions(Guid.Parse(ddlCategory.SelectedValue));
        }
    }
}