using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Forms
{
    public partial class CustomQuestions : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DrawControls();

            if (!IsPostBack)
                Display();
        }

        private void Display()
        {

            var dataContext = new nChangerDb();
            var pdfFormTemplateId = Guid.Parse(CurrentId);
            var generalQuestionUserResponse =
                dataContext.GeneralQuestionUserResponses.Where(
                    g => g.PdfFormTemplateId.Equals(pdfFormTemplateId) && g.UserId.Equals(UserId)).ToList();

            if (generalQuestionUserResponse.Count > 0)
            {

                foreach (HtmlTableRow row in tblFields.Rows)
                {
                    foreach (HtmlTableCell cell in row.Cells)
                    {
                        foreach (var ctl in cell.Controls)
                        {
                            if (ctl.GetType() == typeof (TextBox))
                            {
                                var txt = (TextBox) ctl;
                                var questionId = Guid.Parse(txt.Attributes["DB_ID"].ToString());
                                txt.Text = GetAnswerText(questionId);
                            }
                            else if (ctl.GetType() == typeof (RadioButtonList))
                            {
                                var rdb = (RadioButtonList) ctl;
                                var questionId = Guid.Parse(rdb.Attributes["DB_ID"].ToString());
                                rdb.Items.FindByText(GetAnswerText(questionId)).Selected = true;
                            }
                        }
                    }
                }
            }
        }


        public string GetAnswerText(Guid id)
        {
            var dataContext = new nChangerDb();
            var pdfFormTemplateId = Guid.Parse(CurrentId);

            return (from g in dataContext.GeneralQuestionUserResponses
                    where g.DefineQuestionId.Equals(id) && g.PdfFormTemplateId.Equals(pdfFormTemplateId) && g.UserId.Equals(UserId)
                    select g.UserAnswer).SingleOrDefault();
        }

        //public string GetAnswerCheck(Guid id)
        //{
        //    var dataContext = new nChangerDb();
        //    var pdfFormTemplateId = Guid.Parse(CurrentId);

        //    return (from g in dataContext.GeneralQuestionUserResponses
        //            where g.DefineQuestionId.Equals(id) && g.PdfFormTemplateId.Equals(pdfFormTemplateId) && g.UserId.Equals(UserId)
        //            select g.UserAnswer).SingleOrDefault();
        //}

        private void DrawControls()
        {
            try
            {
                var id = Guid.Parse(CurrentId);

                using (var dataContext = new nChangerDb())
                {
                    var pdfFoemTemplate = dataContext.PdfFormTemplates.Find(id);
                    var questions = pdfFoemTemplate.ProvinceCategory.DefineQuestions;

                    foreach (var question in questions)
                    {
                        switch (question.QuestionType)
                        {
                            case "txt":
                                GenerateTextBox(question);
                                break;
                            case "rdb":
                                GenerateRadioButton(question);
                                break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private void GenerateTextBox(DefineQuestion question)
        {
            var row = new HtmlTableRow();
            row.Attributes.Add("class", "field");
           
            var cellControl = new HtmlTableCell();
            var txt = new TextBox
            {
                ID = question.Id.ToString().Substring(0, 8),
                MaxLength = 500
            };

            txt.Attributes.Add("placeholder", question.Question);
            txt.Attributes.Add("DB_ID", question.Id.ToString());
            cellControl.Controls.Add(txt);
            row.Cells.Add(cellControl);
            tblFields.Rows.Add(row);
        }

        private void GenerateRadioButton(DefineQuestion question)
        {
            var row = new HtmlTableRow();
            row.Attributes.Add("class", "field");

            var cellControl = new HtmlTableCell();

            if (question.QuestionOptions == null)
                return;

            if (question.QuestionOptions.Count > 1)
            {
                var rdList = new RadioButtonList
                {
                    ID = question.Id.ToString().Substring(0, 8),
                    RepeatDirection=RepeatDirection.Horizontal,
                    RepeatColumns=2
                    
                };

                rdList.Attributes.Add("Question",question.Question);
                rdList.Attributes.Add("DB_ID", question.Id.ToString());

                foreach (var option in question.QuestionOptions)
                {
                    rdList.Items.Add(new ListItem
                    {
                        Text = option.OptionLabel,
                        Value = option.Id.ToString()
                    });
                }

                cellControl.InnerHtml ="<label>"+ question.Question+ "</label>"; 
                cellControl.Controls.Add(rdList);
            }
            else
            {
                QuestionOption first = question.QuestionOptions.FirstOrDefault();
                var radio = new RadioButton
                {
                    ID = question.Id.ToString().Substring(0, 8),
                    Text=first.OptionLabel,
                    
                };

                cellControl.Controls.Add(radio);
            }
              
            row.Cells.Add(cellControl);
            tblFields.Rows.Add(row);
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            RemoveExisting();

            foreach (HtmlTableRow row in tblFields.Rows)
            {
                foreach (HtmlTableCell cell in row.Cells)
                {
                    foreach (var ctl in cell.Controls)
                    {
                        if (ctl.GetType() == typeof (TextBox))
                        {
                            var txt = (TextBox)ctl;

                            using (var dataContext=new nChangerDb())
                            {
                                dataContext.GeneralQuestionUserResponses.AddOrUpdate(new GeneralQuestionUserResponse
                                {
                                    Id=Guid.NewGuid(),
                                    Question=txt.Attributes["placeholder"].ToString(),
                                    UserAnswer=txt.Text,
                                    PdfFormTemplateId=Guid.Parse(CurrentId),
                                    UserId=UserId,
                                    DefineQuestionId=Guid.Parse(Convert.ToString(txt.Attributes["DB_ID"])),
                                    EntryDate=DateTime.Now,
                                    EntryIP=CommonFunctions.GetIpAddress(),
                                    EntryId=UserId,
                                    IsActive=true
                                });

                                dataContext.SaveChanges();
                            }

                        }else if (ctl.GetType() == typeof (RadioButtonList))
                        {
                            var rdList = (RadioButtonList)ctl;
                            using (var dataContext = new nChangerDb())
                            {
                                dataContext.GeneralQuestionUserResponses.AddOrUpdate(new GeneralQuestionUserResponse
                                {
                                    Id = Guid.NewGuid(),
                                    Question = rdList.Attributes["Question"].ToString(),
                                    UserAnswer = rdList.SelectedIndex!=-1?rdList.SelectedItem.Text:string.Empty,
                                    PdfFormTemplateId = Guid.Parse(CurrentId),
                                    UserId = UserId,
                                    DefineQuestionId = Guid.Parse(Convert.ToString(rdList.Attributes["DB_ID"])),
                                    EntryDate = DateTime.Now,
                                    EntryIP = CommonFunctions.GetIpAddress(),
                                    EntryId = UserId,
                                    IsActive = true
                                });

                                dataContext.SaveChanges();
                            }
                        }
                    }
                }
            }

             
            var redirect = Sections.Where(s => s.DisplayOrder.Equals(1)).FirstOrDefault().AspxPath;
            Response.Redirect(redirect);
        }

        private void RemoveExisting()
        {
            using (var dataContext = new nChangerDb())
            {
                dataContext.Database.ExecuteSqlCommand(
                    "DELETE FROM GeneralQuestionUserResponse WHERE PdfFormTemplateId='" + CurrentId + "' AND UserId='" +
                    UserId + "'");
            }
        }
    }
}