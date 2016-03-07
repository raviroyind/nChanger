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
    public partial class FrmOnPersonalInfo : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               FormIndex = 1;
               Display();
            }
        }

        private void Display()
        {
            ddlCountryMarried.DataSource = CommonFunctions.GetCountriesList();
            ddlCountryMarried.DataBind();
            ddlCountryMarried.Items.Insert(0, "Select");
             
                try
                {
                    var id = Guid.Parse(RecordId);

                    using (var dataContext = new nChangerDb())
                    {

                        var personalInformation = dataContext.PersonalInformations.Find(id);
                     
                        if (personalInformation != null)
                        { 
                            txtPresentFirstName.Text = personalInformation.PresentFirstName;
                            txtPresentMiddleName.Text = personalInformation.PresentMiddleName;
                            txtPresentLastName.Text = personalInformation.PresentLastName;

                            if(!string.IsNullOrEmpty(personalInformation.Sex))
                               rdListSex.Items.FindByValue(personalInformation.Sex).Selected = true;

                            txtMailAddStreetNo.Text = personalInformation.MailAddStreetNo;
                            txtMailAddPOBox.Text = personalInformation.MailAddPOBox;
                            txtMailAddAptSuitNo.Text = personalInformation.MailAddAptSuitNo;
                            txtMailAddBuzzerNo.Text = personalInformation.MailAddBuzzerNo;
                            txtMailAddCityTownVillage.Text = personalInformation.MailAddCityTownVillage;
                            txtMailAddProvience.Text = personalInformation.MailAddProvience;
                            txtMailAddPostalCode.Text = personalInformation.MailAddPostalCode;
                            txtMailAddHomePhoneCode.Text = personalInformation.MailAddHomePhoneCode;
                            txtMailAddHomePhoneNo.Text = personalInformation.MailAddHomePhoneNo;
                            txtMailAddWorkPhoneCode.Text = personalInformation.MailAddWorkPhoneCode;
                            txtMailAddWorkPhoneNo.Text = personalInformation.MailAddWorkPhoneNo;

                            if (personalInformation.LivedInOntarioYears.HasValue)
                                txtLivedInOntarioYears.Text = personalInformation.LivedInOntarioYears.Value.ToString();

                            if (personalInformation.LivedInOntarioMonths.HasValue)
                                txtLivedInOntarioMonths.Text = personalInformation.LivedInOntarioMonths.Value.ToString();

                            if (!string.IsNullOrWhiteSpace(personalInformation.LivedInOntarioPast12Months))
                                rdListLivedInOntarioPast12Months.Items.FindByValue(personalInformation.LivedInOntarioPast12Months)
                                    .Selected = true;

                            if (personalInformation.DOBMonth > 0 && personalInformation.DOBDay > 0 && personalInformation.DOBYear > 0)
                            {
                                DateTime dt = new DateTime(personalInformation.DOBYear.Value, personalInformation.DOBMonth.Value, personalInformation.DOBDay.Value);
                                txtDOB.Text = dt.ToString("MM/dd/yyyy");
                            }
                                
                            txtBirthCityTownVillage.Text = personalInformation.BirthCityTownVillage;
                            txtBirthProvinceOrState.Text = personalInformation.BirthProvinceOrState;
                            txtBirthCountry.Text = personalInformation.BirthCountry;
                            txtNewFirstName.Text = personalInformation.NewFirstName;
                            txtNewMiddleName.Text = personalInformation.NewMiddleName;
                            txtNewLastName.Text = personalInformation.NewLastName;

                            if (!string.IsNullOrWhiteSpace(personalInformation.Married))
                                rdListMarried.Items.FindByValue(personalInformation.Married).Selected = true;

                            txtPartnerFisrtName.Text = personalInformation.PartnerFisrtName;
                            txtPartnerMiddleName.Text = personalInformation.PartnerMiddleName;
                            txtPartnerLastName.Text = personalInformation.PartnerLastName;

                            if (personalInformation.DateMarriedMonth > 0 && personalInformation.DateMarriedDay > 0 && personalInformation.DateMarriedYear > 0)
                            {
                                DateTime dt = new DateTime(personalInformation.DateMarriedYear.Value, personalInformation.DateMarriedMonth.Value, personalInformation.DateMarriedDay.Value);
                                txtDateMarried.Text = dt.ToString("MM/dd/yyyy");
                            }
                               
                            txtCityTownMarried.Text = personalInformation.CityTownMarried;
                            txtStateOrProvinceMarried.Text = personalInformation.StateOrProvinceMarried;

                            if (!string.IsNullOrWhiteSpace(personalInformation.CountryMarried))
                                ddlCountryMarried.Items.FindByValue(personalInformation.CountryMarried).Selected = true;

                            if (!string.IsNullOrWhiteSpace(personalInformation.JDeclarationSigned))
                                rdListJDeclarationSigned.Items.FindByValue(personalInformation.JDeclarationSigned).Selected = true;

                            txtJDeclarationPersonFirstName.Text = personalInformation.JDeclarationPersonFirstName;
                            txtJDeclarationPersonMiddleName.Text = personalInformation.JDeclarationPersonMiddleName;
                            txtJDeclarationPersonLastName.Text = personalInformation.JDeclarationPersonLastName;

                            if (personalInformation.SentRegistrarMonth > 0 && personalInformation.SentRegistrarDay > 0 &&
                                personalInformation.SentRegistrarYear > 0)
                            {
                                DateTime dt = new DateTime(personalInformation.SentRegistrarYear.Value, personalInformation.SentRegistrarMonth.Value, personalInformation.SentRegistrarDay.Value);
                                txtSentRegistrarDate.Text = dt.ToString("MM/dd/yyyy");
                            }
                                

                            if (!string.IsNullOrWhiteSpace(personalInformation.SubmittedForm4))
                                rdListSubmittedForm4.Items.FindByValue(personalInformation.SubmittedForm4).Selected = true;

                        }
                         
                    }

                }
                catch (Exception)
                { }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
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

            var nextPage = FormIndex + 1;
            var redirect = Sections.Where(s => s.DisplayOrder.Equals(nextPage)).FirstOrDefault().AspxPath;
             
            Response.Redirect(redirect);
        }

        private string Submit()
        {

            var id = Guid.Parse(RecordId);
            var returnMessage = string.Empty;
            try
            {
                using (var dataContext=new nChangerDb())
                {
                    var dbEntry = dataContext.PersonalInformations.Find(id);

                    if (dbEntry != null)
                    {
                        dbEntry.PresentFirstName = txtPresentFirstName.Text;
                        dbEntry.PresentMiddleName = txtPresentMiddleName.Text;
                        dbEntry.PresentLastName = txtPresentLastName.Text;
                        dbEntry.Sex =rdListSex.SelectedIndex==-1?string.Empty: rdListSex.SelectedItem.Value;
                        dbEntry.MailAddStreetNo = txtMailAddStreetNo.Text;
                        dbEntry.MailAddPOBox = txtMailAddPOBox.Text;
                        dbEntry.MailAddAptSuitNo = txtMailAddAptSuitNo.Text;
                        dbEntry.MailAddBuzzerNo = txtMailAddBuzzerNo.Text;
                        dbEntry.MailAddCityTownVillage = txtMailAddCityTownVillage.Text;
                        dbEntry.MailAddProvience = txtMailAddProvience.Text;
                        dbEntry.MailAddPostalCode = txtMailAddPostalCode.Text;
                        dbEntry.MailAddHomePhoneCode = txtMailAddHomePhoneCode.Text;
                        dbEntry.MailAddHomePhoneNo = txtMailAddHomePhoneNo.Text;
                        dbEntry.MailAddWorkPhoneCode = txtMailAddWorkPhoneCode.Text;
                        dbEntry.MailAddWorkPhoneNo = txtMailAddWorkPhoneNo.Text;
                        dbEntry.LivedInOntarioYears = string.IsNullOrEmpty(txtLivedInOntarioYears.Text)?0:Convert.ToInt32(txtLivedInOntarioYears.Text);
                        dbEntry.LivedInOntarioMonths = string.IsNullOrEmpty(txtLivedInOntarioMonths.Text) ? 0 : Convert.ToInt32(txtLivedInOntarioMonths.Text);
                        dbEntry.LivedInOntarioPast12Months = rdListLivedInOntarioPast12Months.SelectedIndex==-1?string.Empty : rdListLivedInOntarioPast12Months.SelectedValue;
                        dbEntry.DOBYear = string.IsNullOrEmpty(txtDOB.Text) ? 0 : DateTime.ParseExact(txtDOB.Text, "MM/dd/yyyy", null).Year;
                        dbEntry.DOBMonth = string.IsNullOrEmpty(txtDOB.Text) ? 0 : DateTime.ParseExact(txtDOB.Text, "MM/dd/yyyy", null).Month;
                        dbEntry.DOBDay = string.IsNullOrEmpty(txtDOB.Text) ? 0 : DateTime.ParseExact(txtDOB.Text, "MM/dd/yyyy", null).Day;
                        dbEntry.BirthCityTownVillage = txtBirthCityTownVillage.Text;
                        dbEntry.BirthProvinceOrState = txtBirthProvinceOrState.Text;
                        dbEntry.BirthCountry = txtBirthCountry.Text;
                        dbEntry.NewFirstName = txtNewFirstName.Text;
                        dbEntry.NewMiddleName = txtNewMiddleName.Text;
                        dbEntry.NewLastName = txtNewLastName.Text;
                        dbEntry.Married = rdListMarried.SelectedValue;
                        dbEntry.PartnerFisrtName = txtPartnerFisrtName.Text;
                        dbEntry.PartnerMiddleName = txtPartnerMiddleName.Text;
                        dbEntry.PartnerLastName = txtPartnerLastName.Text;
                        dbEntry.DateMarriedMonth = string.IsNullOrEmpty(txtDateMarried.Text) ? 0 : DateTime.ParseExact(txtDateMarried.Text, "MM/dd/yyyy",null).Month;
                        dbEntry.DateMarriedDay = string.IsNullOrEmpty(txtDateMarried.Text) ? 0 : DateTime.ParseExact(txtDateMarried.Text, "MM/dd/yyyy",null).Day;
                        dbEntry.DateMarriedYear = string.IsNullOrEmpty(txtDateMarried.Text) ? 0 : DateTime.ParseExact(txtDateMarried.Text, "MM/dd/yyyy", null).Year;
                        dbEntry.CityTownMarried = txtCityTownMarried.Text;
                        dbEntry.StateOrProvinceMarried = txtStateOrProvinceMarried.Text;
                        dbEntry.CountryMarried =ddlCountryMarried.SelectedIndex==-1?string.Empty:ddlCountryMarried.SelectedValue;
                        dbEntry.JDeclarationSigned = rdListJDeclarationSigned.SelectedIndex == -1 ? string.Empty : rdListJDeclarationSigned.SelectedValue;
                        dbEntry.JDeclarationPersonFirstName = txtJDeclarationPersonFirstName.Text;
                        dbEntry.JDeclarationPersonMiddleName = txtJDeclarationPersonMiddleName.Text;
                        dbEntry.JDeclarationPersonLastName = txtJDeclarationPersonLastName.Text;
                        dbEntry.SentRegistrarMonth = string.IsNullOrEmpty(txtSentRegistrarDate.Text) ? 0 : DateTime.ParseExact(txtSentRegistrarDate.Text, "MM/dd/yyyy", null).Month;
                        dbEntry.SentRegistrarDay = string.IsNullOrEmpty(txtSentRegistrarDate.Text) ? 0 : Convert.ToDateTime(txtSentRegistrarDate.Text).Day;
                        dbEntry.SentRegistrarYear = string.IsNullOrEmpty(txtSentRegistrarDate.Text) ? 0 : Convert.ToDateTime(txtSentRegistrarDate.Text).Year;
                        dbEntry.SubmittedForm4 = rdListSubmittedForm4.SelectedIndex == -1 ? string.Empty : rdListSubmittedForm4.SelectedValue;
                        dbEntry.EntryDate= DateTime.Today;
                        dbEntry.EntryIP= CommonFunctions.GetIpAddress();
                        dbEntry.IsActive= true;
                        dbEntry.EntryId = Convert.ToString(Session["USER_KEY"]);

                        dataContext.Entry(dbEntry).State = EntityState.Modified;
                    }
                    else
                    {
                        var entry = new PersonalInformation
                        {
                            Id=id,
                            PdfFormTemplateId = Guid.Parse(CurrentId),
                            UserId = Convert.ToString(Session["USER_KEY"]),
                            PresentFirstName = txtPresentFirstName.Text,
                            PresentMiddleName = txtPresentMiddleName.Text,
                            PresentLastName = txtPresentLastName.Text,
                            Sex = rdListSex.SelectedIndex == -1 ? string.Empty : rdListSex.SelectedItem.Value,
                            MailAddStreetNo = txtMailAddStreetNo.Text,
                            MailAddPOBox = txtMailAddPOBox.Text,
                            MailAddAptSuitNo = txtMailAddAptSuitNo.Text,
                            MailAddBuzzerNo = txtMailAddBuzzerNo.Text,
                            MailAddCityTownVillage = txtMailAddCityTownVillage.Text,
                            MailAddProvience = txtMailAddProvience.Text,
                            MailAddPostalCode = txtMailAddPostalCode.Text,
                            MailAddHomePhoneCode = txtMailAddHomePhoneCode.Text,
                            MailAddHomePhoneNo = txtMailAddHomePhoneNo.Text,
                            MailAddWorkPhoneCode = txtMailAddWorkPhoneCode.Text,
                            MailAddWorkPhoneNo = txtMailAddWorkPhoneNo.Text,
                            LivedInOntarioYears = string.IsNullOrEmpty(txtLivedInOntarioYears.Text) ? 0 : Convert.ToInt32(txtLivedInOntarioYears.Text),
                            LivedInOntarioMonths = string.IsNullOrEmpty(txtLivedInOntarioMonths.Text) ? 0 : Convert.ToInt32(txtLivedInOntarioMonths.Text),
                            LivedInOntarioPast12Months = rdListLivedInOntarioPast12Months.SelectedIndex == -1 ? string.Empty : rdListLivedInOntarioPast12Months.SelectedValue,
                            DOBYear = string.IsNullOrEmpty(txtDOB.Text) ? 0 : DateTime.ParseExact(txtDOB.Text, "MM/dd/yyyy", null).Year,
                            DOBMonth = string.IsNullOrEmpty(txtDOB.Text) ? 0 : DateTime.ParseExact(txtDOB.Text, "MM/dd/yyyy", null).Month,
                            DOBDay = string.IsNullOrEmpty(txtDOB.Text) ? 0 : DateTime.ParseExact(txtDOB.Text, "MM/dd/yyyy", null).Day,
                            BirthCityTownVillage = txtBirthCityTownVillage.Text,
                            BirthProvinceOrState = txtBirthProvinceOrState.Text,
                            BirthCountry = txtBirthCountry.Text,
                            NewFirstName = txtNewFirstName.Text,
                            NewMiddleName = txtNewMiddleName.Text,
                            NewLastName = txtNewLastName.Text,
                            Married = rdListMarried.SelectedValue,
                            PartnerFisrtName = txtPartnerFisrtName.Text,
                            PartnerMiddleName = txtPartnerMiddleName.Text,
                            PartnerLastName = txtPartnerLastName.Text,
                            DateMarriedMonth = string.IsNullOrEmpty(txtDateMarried.Text) ? 0 : DateTime.ParseExact(txtDateMarried.Text, "MM/dd/yyyy", null).Month,
                            DateMarriedDay = string.IsNullOrEmpty(txtDateMarried.Text) ? 0 : DateTime.ParseExact(txtDateMarried.Text, "MM/dd/yyyy", null).Day,
                            DateMarriedYear = string.IsNullOrEmpty(txtDateMarried.Text) ? 0 : DateTime.ParseExact(txtDateMarried.Text, "MM/dd/yyyy", null).Year,
                            CityTownMarried = txtCityTownMarried.Text,
                            StateOrProvinceMarried = txtStateOrProvinceMarried.Text,
                            CountryMarried = ddlCountryMarried.SelectedIndex == -1 ? string.Empty : ddlCountryMarried.SelectedValue,
                            JDeclarationSigned = rdListJDeclarationSigned.SelectedIndex == -1 ? string.Empty : rdListJDeclarationSigned.SelectedValue,
                            JDeclarationPersonFirstName = txtJDeclarationPersonFirstName.Text,
                            JDeclarationPersonMiddleName = txtJDeclarationPersonMiddleName.Text,
                            JDeclarationPersonLastName = txtJDeclarationPersonLastName.Text,
                            SentRegistrarMonth = string.IsNullOrEmpty(txtSentRegistrarDate.Text) ? 0 : DateTime.ParseExact(txtSentRegistrarDate.Text, "MM/dd/yyyy", null).Month,
                            SentRegistrarDay = string.IsNullOrEmpty(txtSentRegistrarDate.Text) ? 0 : Convert.ToDateTime(txtSentRegistrarDate.Text).Day,
                            SentRegistrarYear = string.IsNullOrEmpty(txtSentRegistrarDate.Text) ? 0 : Convert.ToDateTime(txtSentRegistrarDate.Text).Year,
                            SubmittedForm4 = rdListSubmittedForm4.SelectedIndex == -1 ? string.Empty : rdListSubmittedForm4.SelectedValue,
                            EntryDate = DateTime.Today,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            IsActive = true,
                            EntryId = Convert.ToString(Session["USER_KEY"])
                        };

                        dataContext.PersonalInformations.Add(entry);
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
         
    }
}