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
                    var id = Guid.Parse(CurrentId);

                    using (var dataContext = new nChangerDb())
                    {
                        var pdfFoemTemplate = dataContext.PdfFormTemplates.Find(id);
                        var questions = pdfFoemTemplate.ProvinceCategory.DefineQuestions;

                    var frmOn =
                            dataContext.PersonalInformations.FirstOrDefault(
                                f => f.UserId.Equals(UserId) && f.PdfFormTemplateId.Equals(id));

                        

                        if (frmOn != null)
                        {
                            btnPreviewPdf.CssClass = string.Empty;
                            btnPreviewPdf.CssClass = "btn btn-sm btn-primary";

                            txtPresentFirstName.Text = frmOn.PresentFirstName;
                            txtPresentMiddleName.Text = frmOn.PresentMiddleName;
                            txtPresentLastName.Text = frmOn.PresentLastName;

                            if(!string.IsNullOrEmpty(frmOn.Sex))
                               rdListSex.Items.FindByValue(frmOn.Sex).Selected = true;

                            txtMailAddStreetNo.Text = frmOn.MailAddStreetNo;
                            txtMailAddPOBox.Text = frmOn.MailAddPOBox;
                            txtMailAddAptSuitNo.Text = frmOn.MailAddAptSuitNo;
                            txtMailAddBuzzerNo.Text = frmOn.MailAddBuzzerNo;
                            txtMailAddCityTownVillage.Text = frmOn.MailAddCityTownVillage;
                            txtMailAddProvience.Text = frmOn.MailAddProvience;
                            txtMailAddPostalCode.Text = frmOn.MailAddPostalCode;
                            txtMailAddHomePhoneCode.Text = frmOn.MailAddHomePhoneCode;
                            txtMailAddHomePhoneNo.Text = frmOn.MailAddHomePhoneNo;
                            txtMailAddWorkPhoneCode.Text = frmOn.MailAddWorkPhoneCode;
                            txtMailAddWorkPhoneNo.Text = frmOn.MailAddWorkPhoneNo;

                            if (frmOn.LivedInOntarioYears.HasValue)
                                txtLivedInOntarioYears.Text = frmOn.LivedInOntarioYears.Value.ToString();

                            if (frmOn.LivedInOntarioMonths.HasValue)
                                txtLivedInOntarioMonths.Text = frmOn.LivedInOntarioMonths.Value.ToString();

                            if (!string.IsNullOrWhiteSpace(frmOn.LivedInOntarioPast12Months))
                                rdListLivedInOntarioPast12Months.Items.FindByValue(frmOn.LivedInOntarioPast12Months)
                                    .Selected = true;

                            if (frmOn.DOBMonth > 0 && frmOn.DOBDay > 0 && frmOn.DOBYear > 0)
                            {
                                DateTime dt = new DateTime(frmOn.DOBYear.Value, frmOn.DOBMonth.Value, frmOn.DOBDay.Value);
                                txtDOB.Text = dt.ToString("MM/dd/yyyy");
                            }
                                
                            txtBirthCityTownVillage.Text = frmOn.BirthCityTownVillage;
                            txtBirthProvinceOrState.Text = frmOn.BirthProvinceOrState;
                            txtBirthCountry.Text = frmOn.BirthCountry;
                            txtNewFirstName.Text = frmOn.NewFirstName;
                            txtNewMiddleName.Text = frmOn.NewMiddleName;
                            txtNewLastName.Text = frmOn.NewLastName;

                            if (!string.IsNullOrWhiteSpace(frmOn.Married))
                                rdListMarried.Items.FindByValue(frmOn.Married).Selected = true;

                            txtPartnerFisrtName.Text = frmOn.PartnerFisrtName;
                            txtPartnerMiddleName.Text = frmOn.PartnerMiddleName;
                            txtPartnerLastName.Text = frmOn.PartnerLastName;

                            if (frmOn.DateMarriedMonth > 0 && frmOn.DateMarriedDay > 0 && frmOn.DateMarriedYear > 0)
                            {
                                DateTime dt = new DateTime(frmOn.DateMarriedYear.Value, frmOn.DateMarriedMonth.Value, frmOn.DateMarriedDay.Value);
                                txtDateMarried.Text = dt.ToString("MM/dd/yyyy");
                            }
                               
                            txtCityTownMarried.Text = frmOn.CityTownMarried;
                            txtStateOrProvinceMarried.Text = frmOn.StateOrProvinceMarried;

                            if (!string.IsNullOrWhiteSpace(frmOn.CountryMarried))
                                ddlCountryMarried.Items.FindByValue(frmOn.CountryMarried).Selected = true;

                            if (!string.IsNullOrWhiteSpace(frmOn.JDeclarationSigned))
                                rdListJDeclarationSigned.Items.FindByValue(frmOn.JDeclarationSigned).Selected = true;

                            txtJDeclarationPersonFirstName.Text = frmOn.JDeclarationPersonFirstName;
                            txtJDeclarationPersonMiddleName.Text = frmOn.JDeclarationPersonMiddleName;
                            txtJDeclarationPersonLastName.Text = frmOn.JDeclarationPersonLastName;

                            if (frmOn.SentRegistrarMonth > 0 && frmOn.SentRegistrarDay > 0 &&
                                frmOn.SentRegistrarYear > 0)
                            {
                                DateTime dt = new DateTime(frmOn.SentRegistrarYear.Value, frmOn.SentRegistrarMonth.Value, frmOn.SentRegistrarDay.Value);
                                txtSentRegistrarDate.Text = dt.ToString("MM/dd/yyyy");
                            }
                                

                            if (!string.IsNullOrWhiteSpace(frmOn.SubmittedForm4))
                                rdListSubmittedForm4.Items.FindByValue(frmOn.SubmittedForm4).Selected = true;

                        }
                        else
                        {
                            btnPreviewPdf.CssClass = string.Empty;
                            btnPreviewPdf.CssClass = "btn btn-sm btn-primary disabled";
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

            var id = Guid.Parse(CurrentId);
            var returnMessage = string.Empty;
            try
            {
                using (var dataContext=new nChangerDb())
                {
                     var dbEntry =
                            dataContext.PersonalInformations.FirstOrDefault(
                                f => f.UserId.Equals(UserId) && f.PdfFormTemplateId.Equals(id));
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
                            Id=Guid.NewGuid(),
                            PdfFormTemplateId = id,
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
                            dataContext.PersonalInformations.FirstOrDefault(
                                f => f.UserId.Equals(UserId) && f.PdfFormTemplateId.Equals(id));

                if (frmOn != null)
                {
                    var file = new FileInfo(PdfInjector.FillForm(id,UserId));

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