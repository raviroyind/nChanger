using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using nameChanger.WebUI;
using nChanger.Core;


namespace nChanger.WebUI.Forms
{
    public partial class FrmParentInformation : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormIndex = 2;
                Display();
            }
        }

        private void Display()
        {
            try
            {
                var id = Guid.Parse(CurrentId);

                hypBack.NavigateUrl = "../Forms/frmONPersonalInfo.aspx?id=" + CurrentId;

                using (var dataContext = new nChangerDb())
                {
                    var frmParentInformation =
                        dataContext.ParentInformations.FirstOrDefault(
                            f => f.UserId.Equals(UserId) && f.PdfFormTemplateId.Equals(id));

                    if (frmParentInformation != null)
                    {
                        btnPreviewPdf.CssClass = string.Empty;
                        btnPreviewPdf.CssClass = "btn btn-sm btn-primary";

                        txtFatherFirstName.Text = frmParentInformation.FatherFirstName;	
                        txtFatherMiddleName.Text = frmParentInformation.FatherMiddleName;
                        txtFatherLastName.Text = frmParentInformation.FatherLastName;
                        txtFatherLastNameOther.Text = frmParentInformation.FatherOtherLastName;
                        txtMotherFirstName.Text = frmParentInformation.MotherFirstName;
                        txtMotherMiddleName.Text = frmParentInformation.MotherMiddleName;
                        txtMotherLastNameBorn.Text = frmParentInformation.MotherLastNameWhenBorn;
                        txtMotherLastNamePresent.Text = frmParentInformation.MotherLastNamePresent;
                        txtMotherLastNameOther.Text = frmParentInformation.MotherLastNameOther;
                    }
                    else
                    {
                        btnPreviewPdf.CssClass = string.Empty;
                        btnPreviewPdf.CssClass = "btn btn-sm btn-primary disabled";
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

            try
            {
                divMsg.InnerText = Submit();
                var sections = Sections;
                var page = Path.GetFileName(Request.PhysicalPath);
                var curret = Sections.FirstOrDefault(s => s.AspxPath.Contains(page));

                using (var dataContext = new nChangerDb())
                {

                    dataContext.UserFormDetails.AddOrUpdate(new UserFormDetail
                    {
                        Id = Guid.NewGuid(),
                        UserId = UserId,
                        AspxPath = curret.AspxPath,
                        TableName = curret.TableName,
                        PdfTemplateId = Guid.Parse(CurrentId),
                        FrmGuid = curret.FrmGuid,
                        Completed = "Y"
                    });

                    dataContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            var nextPage = FormIndex + 1;
            var redirect = Sections.Where(s => s.DisplayOrder.Equals(nextPage)).FirstOrDefault().AspxPath;

            Response.Redirect(redirect);
        }

        private string Submit()
        {

            var id = Guid.Parse(CurrentId);
            var returnMessage = string.Empty;
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry =
                        dataContext.ParentInformations.FirstOrDefault(
                            f => f.UserId.Equals(UserId) && f.PdfFormTemplateId.Equals(id));


                    if (dbEntry != null)
                    {
                         dbEntry.FatherFirstName = txtFatherFirstName.Text;
                         dbEntry.FatherMiddleName = txtFatherMiddleName.Text;
                         dbEntry.FatherLastName = txtFatherLastName.Text;
                         dbEntry.FatherOtherLastName = txtFatherLastNameOther.Text;
                         dbEntry.MotherFirstName = txtMotherFirstName.Text;
                         dbEntry.MotherMiddleName = txtMotherMiddleName.Text;
                         dbEntry.MotherLastNameWhenBorn = txtMotherLastNameBorn.Text;
                         dbEntry.MotherLastNamePresent = txtMotherLastNamePresent.Text;
                         dbEntry.MotherLastNameOther = txtMotherLastNameOther.Text;
                         dbEntry.EntryDate = DateTime.Today;
                         dbEntry.EntryIP = CommonFunctions.GetIpAddress();
                         dbEntry.IsActive = true;
                         dbEntry.EntryId = UserId;

                         dataContext.Entry(dbEntry).State = EntityState.Modified;
                    }
                    else
                    {
                        var entry = new ParentInformation()
                        {
                            Id = Guid.NewGuid(),
                            PdfFormTemplateId = id,
                            UserId = UserId,
                            FatherFirstName = txtFatherFirstName.Text,
                            FatherMiddleName = txtFatherMiddleName.Text,
                            FatherLastName = txtFatherLastName.Text,
                            FatherOtherLastName = txtFatherLastNameOther.Text,
                            MotherFirstName = txtMotherFirstName.Text,
                            MotherMiddleName = txtMotherMiddleName.Text,
                            MotherLastNameWhenBorn = txtMotherLastNameBorn.Text,
                            MotherLastNamePresent = txtMotherLastNamePresent.Text,
                            MotherLastNameOther = txtMotherLastNameOther.Text,
                            EntryDate = DateTime.Today,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            IsActive = true,
                            EntryId = UserId
                        };

                         dataContext.ParentInformations.Add(entry);
 
                    }
                     
                    dataContext.SaveChanges();

                    returnMessage = "Data submitted successfully!";
                    btnPreviewPdf.CssClass = string.Empty;
                    btnPreviewPdf.CssClass = "btn btn-sm btn-primary";
                }
            }
            catch (DbEntityValidationException ex)
            {
                returnMessage = ex.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors).Aggregate(returnMessage, (current, ve) => current + (ve.PropertyName + " Error Msg:" + ve.ErrorMessage));
            }

            return returnMessage;
        }

        protected void btnPreviewPdf_OnClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(CurrentId);
            using (var dataContext = new nChangerDb())
            {
                var frmOn =
                            dataContext.ParentInformations.FirstOrDefault(
                                f => f.UserId.Equals(UserId) && f.PdfFormTemplateId.Equals(id));

                if (frmOn != null)
                {
                    var file = new FileInfo(PdfInjector.FillForm(id, UserId));

                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.Flush();
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }
            }
        }
  
    }
}