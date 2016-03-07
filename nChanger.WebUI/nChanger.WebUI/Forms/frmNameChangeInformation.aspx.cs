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
                var id = Guid.Parse(RecordId);

                using (var dataContext = new nChangerDb())
                {
                    var nameChangeInformation = dataContext.NameChangeInformations.Find(id);

                    if (nameChangeInformation != null)
                    {
                         
                        txtResonForNameChange.Text = nameChangeInformation.ResonForNameChange;

                        if (!string.IsNullOrEmpty(nameChangeInformation.ChangedNamePriviously))
                            rdListChangedNamePriviously.Items.FindByValue(
                                nameChangeInformation.ChangedNamePriviously).Selected = true;


                        if (nameChangeInformation.PreviousNameChangeMonth > 0 && nameChangeInformation.PreviousNameChangeDay > 0 && nameChangeInformation.PreviousNameChangeYear > 0)
                        {
                            DateTime dt = new DateTime(nameChangeInformation.PreviousNameChangeYear.Value, nameChangeInformation.PreviousNameChangeMonth.Value, nameChangeInformation.PreviousNameChangeDay.Value);
                            txtPreviousNameChangeDate.Text = dt.ToString("MM/dd/yyyy");
                        }

                             
                        txtPreviousFirstName.Text =  nameChangeInformation.PreviousFirstName;
                        txtPreviousMiddleName.Text= nameChangeInformation.PreviousMiddleName;
                        txtPreviousLastName.Text = nameChangeInformation.PreviousLastName;
                        txtFirstNameAfterChange.Text = nameChangeInformation.FirstNameAfterChange;
                        txtMiddleNameAfterChange.Text = nameChangeInformation.MiddleNameAfterChange;
                        txtLastNameAfterChange.Text = nameChangeInformation.LastNameAfterChange;
                        txtPreviousNameChangeProvince.Text = nameChangeInformation.PreviousNameChangeProvince;

                        if (!string.IsNullOrEmpty(nameChangeInformation.PreviousNameChangeCountry))
                            ddlCountry.Items.FindByValue(nameChangeInformation.PreviousNameChangeCountry)
                                .Selected = true;

                        if(nameChangeInformation.AppliedForChangeAndRefused != null && nameChangeInformation.AppliedForChangeAndRefused.Value)
                            rdLstAppliedForChangeAndRefused.SelectedIndex = 0;
                        else
                            rdLstAppliedForChangeAndRefused.SelectedIndex = 1;

                    }
                    
                }
            }
            catch (Exception)
            {

            }
            
        }

        private string Submit()
        {
            var id = Guid.Parse(RecordId);
            var returnMessage = string.Empty;
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = dataContext.NameChangeInformations.Find(id);
                     
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
                            Id =id,
                            PdfFormTemplateId = Guid.Parse(CurrentId),
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
                    dataContext.Database.ExecuteSqlCommand("DELETE FROM UserFormDetail WHERE UserId='" + UserId +
                                                           "' AND PdfTemplateId='" + CurrentId + "'  AND FrmGuid='" +
                                                           curret.FrmGuid.ToString() + "'");

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
    }
}