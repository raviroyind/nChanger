using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using nameChanger.WebUI;
using nChanger.Core;

namespace nChanger.WebUI.Forms
{
    public partial class FrmNameChangeInformation : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormIndex = 3;
                Display();
            }
        }

        private void Display()
        {
            ddlCountry.DataSource = CommonFunctions.GetCountriesList();
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "Select");
    
            try
            {
                hypBack.NavigateUrl = "../Forms/frmParentInformation.aspx?id=" + CurrentId;
                var id = Guid.Parse(CurrentId);

                using (var dataContext = new nChangerDb())
                {
                    var frmNameChangeInformation =
                        dataContext.NameChangeInformations.FirstOrDefault(
                            f => f.UserId.Equals(UserId) && f.PdfFormTemplateId.Equals(id));

                    if (frmNameChangeInformation != null)
                    {
                        btnPreviewPdf.CssClass = string.Empty;
                        btnPreviewPdf.CssClass = "btn btn-sm btn-primary";

                        txtResonForNameChange.Text = frmNameChangeInformation.ResonForNameChange;

                        if (!string.IsNullOrEmpty(frmNameChangeInformation.ChangedNamePriviously))
                            rdListChangedNamePriviously.Items.FindByValue(
                                frmNameChangeInformation.ChangedNamePriviously).Selected = true;


                        if (frmNameChangeInformation.PreviousNameChangeMonth > 0 && frmNameChangeInformation.PreviousNameChangeDay > 0 && frmNameChangeInformation.PreviousNameChangeYear > 0)
                        {
                            DateTime dt = new DateTime(frmNameChangeInformation.PreviousNameChangeYear.Value, frmNameChangeInformation.PreviousNameChangeMonth.Value, frmNameChangeInformation.PreviousNameChangeDay.Value);
                            txtPreviousNameChangeDate.Text = dt.ToString("MM/dd/yyyy");
                        }

                             
                        txtPreviousFirstName.Text =  frmNameChangeInformation.PreviousFirstName;
                        txtPreviousMiddleName.Text= frmNameChangeInformation.PreviousMiddleName;
                        txtPreviousLastName.Text = frmNameChangeInformation.PreviousLastName;
                        txtFirstNameAfterChange.Text = frmNameChangeInformation.FirstNameAfterChange;
                        txtMiddleNameAfterChange.Text = frmNameChangeInformation.MiddleNameAfterChange;
                        txtLastNameAfterChange.Text = frmNameChangeInformation.LastNameAfterChange;
                        txtPreviousNameChangeProvince.Text = frmNameChangeInformation.PreviousNameChangeProvince;

                        if (!string.IsNullOrEmpty(frmNameChangeInformation.PreviousNameChangeCountry))
                            ddlCountry.Items.FindByValue(frmNameChangeInformation.PreviousNameChangeCountry)
                                .Selected = true;

                        if(frmNameChangeInformation.AppliedForChangeAndRefused != null && frmNameChangeInformation.AppliedForChangeAndRefused.Value)
                            rdLstAppliedForChangeAndRefused.SelectedIndex = 0;
                        else
                            rdLstAppliedForChangeAndRefused.SelectedIndex = 1;

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

        private string Submit()
        {
            var id = Guid.Parse(CurrentId);
            var returnMessage = string.Empty;
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry =
                        dataContext.NameChangeInformations.FirstOrDefault(
                            f => f.UserId.Equals(UserId) && f.PdfFormTemplateId.Equals(id));
                     
                    if (dbEntry != null)
                    {
                        dbEntry.ResonForNameChange = txtResonForNameChange.Text;
                        dbEntry.ChangedNamePriviously = rdListChangedNamePriviously.SelectedIndex != -1
                            ? rdListChangedNamePriviously.SelectedValue:string.Empty;

                        dbEntry.UserId = UserId;
                        dbEntry.PreviousNameChangeDay = string.IsNullOrEmpty(txtPreviousNameChangeDate.Text) ? 0 : DateTime.ParseExact(txtPreviousNameChangeDate.Text, "MM/dd/yyyy", null).Day;
                        dbEntry.PreviousNameChangeMonth = string.IsNullOrEmpty(txtPreviousNameChangeDate.Text) ? 0 : DateTime.ParseExact(txtPreviousNameChangeDate.Text, "MM/dd/yyyy", null).Month;
                        dbEntry.PreviousNameChangeYear = string.IsNullOrEmpty(txtPreviousNameChangeDate.Text) ? 0 : DateTime.ParseExact(txtPreviousNameChangeDate.Text, "MM/dd/yyyy", null).Year;
                        dbEntry.PreviousFirstName = txtPreviousFirstName.Text;
                        dbEntry.PreviousMiddleName = txtPreviousMiddleName.Text;
                        dbEntry.PreviousLastName = txtPreviousLastName.Text;
                        dbEntry.FirstNameAfterChange = txtFirstNameAfterChange.Text;
                        dbEntry.MiddleNameAfterChange = txtMiddleNameAfterChange.Text;
                        dbEntry.LastNameAfterChange = txtLastNameAfterChange.Text;
                        dbEntry.PreviousNameChangeProvince = txtPreviousNameChangeProvince.Text;
                        dbEntry.PreviousNameChangeCountry = ddlCountry.SelectedIndex != -1 ? ddlCountry.SelectedValue:string.Empty;
                        dbEntry.AppliedForChangeAndRefused = rdLstAppliedForChangeAndRefused.SelectedIndex == 0;
                        dbEntry.EntryDate = DateTime.Today;
                        dbEntry.EntryIP = CommonFunctions.GetIpAddress();
                        dbEntry.IsActive = true;
                        dbEntry.EntryId = UserId;

                        dataContext.Entry(dbEntry).State = EntityState.Modified;
                    }
                    else
                    {
                        var entry = new NameChangeInformation()
                        {
                            Id = Guid.NewGuid(),
                            PdfFormTemplateId = id,
                            UserId = UserId,
                            ResonForNameChange = txtResonForNameChange.Text,
                            ChangedNamePriviously = rdListChangedNamePriviously.SelectedIndex != -1? rdListChangedNamePriviously.SelectedValue:string.Empty,
                            PreviousNameChangeDay = string.IsNullOrEmpty(txtPreviousNameChangeDate.Text) ? 0 : DateTime.ParseExact(txtPreviousNameChangeDate.Text, "MM/dd/yyyy", null).Day,
                            PreviousNameChangeMonth = string.IsNullOrEmpty(txtPreviousNameChangeDate.Text) ? 0 : DateTime.ParseExact(txtPreviousNameChangeDate.Text, "MM/dd/yyyy", null).Month,
                            PreviousNameChangeYear = string.IsNullOrEmpty(txtPreviousNameChangeDate.Text) ? 0 : DateTime.ParseExact(txtPreviousNameChangeDate.Text, "MM/dd/yyyy", null).Year,
                            PreviousFirstName = txtPreviousFirstName.Text,
                            PreviousMiddleName = txtPreviousMiddleName.Text,
                            PreviousLastName = txtPreviousLastName.Text,
                            FirstNameAfterChange = txtFirstNameAfterChange.Text,
                            MiddleNameAfterChange = txtMiddleNameAfterChange.Text,
                            LastNameAfterChange = txtLastNameAfterChange.Text,
                            PreviousNameChangeProvince = txtPreviousNameChangeProvince.Text,
                            PreviousNameChangeCountry = ddlCountry.SelectedIndex != -1 ? ddlCountry.SelectedValue:string.Empty,
                            AppliedForChangeAndRefused = rdLstAppliedForChangeAndRefused.SelectedIndex == 0,
                            EntryDate = DateTime.Today,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            IsActive = true,
                            EntryId = UserId
                        };

                        dataContext.NameChangeInformations.Add(entry);
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

        protected void btnPreviewPdf_OnClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(CurrentId);
            using (var dataContext = new nChangerDb())
            {
                var frmOn = dataContext.NameChangeInformations.FirstOrDefault(
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