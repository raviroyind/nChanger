using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using nameChanger.WebUI;
using nChanger.Core;

namespace nChanger.WebUI.Forms
{
    public partial class FrmFinancialInformation : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormIndex = 5;
                Display();
            }
        } 

        private void Display()
        { 
            try
            {
                hypBack.NavigateUrl = "../Forms/frmCriminalOffenceInformation.aspx?id=" + CurrentId;
                var id = Guid.Parse(RecordId);

                using (var dataContext = new nChangerDb())
                {
                    var financialInformation = dataContext.FinancialInformations.Find(id);

                    if (financialInformation != null)
                    {
                        rdLstCourtOrTribunalOrder.SelectedIndex = financialInformation.CourtOrTribunalOrder
                            ? 0
                            : 1;

                        txtCourtFileNumber.Text = financialInformation.CourtFileNumber;
                        txtNameOfCourt.Text = financialInformation.NameOfCourt;

                        if (financialInformation.DateOfCourtOrderDay > 0 &&
                            financialInformation.DateOfCourtOrderMonth > 0 &&
                            financialInformation.DateOfCourtOrderYear > 0)
                        {
                            DateTime dt = new DateTime(financialInformation.DateOfCourtOrderYear.Value,
                                financialInformation.DateOfCourtOrderMonth.Value,
                                financialInformation.DateOfCourtOrderDay.Value);
                            txtDateCourtOrder.Text = dt.ToString("MM/dd/yyyy");
                        }

                        txtNameOfPersonWhoSuedYou.Text = financialInformation.NameOfPersonWhoSuedYou;
                        txtAddressCourtTribunal.Text = financialInformation.AddressCourtTribunal;

                        rdLstSheriffDirected.SelectedIndex = financialInformation.SheriffDirected ? 0 : 1;
                        txtWritNumber.Text = financialInformation.WritNumber;
                        txtNameOfSherrif.Text = financialInformation.NameOfSherrif;
                        txtAddressOfSheriff.Text = financialInformation.AddressOfSheriff;

                        rdLstLiensOrSecurityInterests.SelectedIndex =
                            financialInformation.LiensOrSecurityInterests ? 0 : 1;
                        txtLiensOrSecurityInterestsNameOfPerson.Text =
                            financialInformation.LiensOrSecurityInterestsNameOfPerson;
                        txtAmountOfMoneyOwed.Text = financialInformation.AmountOfMoneyOwed;
                        txtRegitrationNumber.Text = financialInformation.RegitrationNumber;

                        rdLstFinancialStatementsRegistered.SelectedIndex =
                            financialInformation.FinancialStatementsRegistered ? 0 : 1;
                        txtFinancialStatementsRegitrationNumber.Text =
                            financialInformation.FinancialStatementsRegitrationNumber;

                        rdLstUndischargedBankrupt.SelectedIndex = financialInformation.UndischargedBankrupt
                            ? 0
                            : 1;
                        txtDetailsOfBankruptcy.Text = financialInformation.DetailsOfBankruptcy;
                    }
                }
            }
            catch (Exception ex)
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
                    var dbEntry = dataContext.FinancialInformations.Find(id);
                     
                    if (dbEntry != null)
                    {
                        dbEntry.UserId = UserId;
                        dbEntry.CourtOrTribunalOrder = rdLstCourtOrTribunalOrder.SelectedIndex == 0;
                        dbEntry.CourtFileNumber = txtCourtFileNumber.Text;
                        dbEntry.NameOfCourt = txtNameOfCourt.Text;
                        dbEntry.DateOfCourtOrderDay = string.IsNullOrEmpty(txtDateCourtOrder.Text)
                            ? 0
                            : DateTime.ParseExact(txtDateCourtOrder.Text, "MM/dd/yyyy", null).Day;
                        dbEntry.DateOfCourtOrderMonth = string.IsNullOrEmpty(txtDateCourtOrder.Text)
                            ? 0
                            : DateTime.ParseExact(txtDateCourtOrder.Text, "MM/dd/yyyy", null).Month;
                        dbEntry.DateOfCourtOrderYear = string.IsNullOrEmpty(txtDateCourtOrder.Text)
                            ? 0
                            : DateTime.ParseExact(txtDateCourtOrder.Text, "MM/dd/yyyy", null).Year;
                        dbEntry.NameOfPersonWhoSuedYou = txtNameOfPersonWhoSuedYou.Text;
                        dbEntry.AddressCourtTribunal = txtAddressCourtTribunal.Text;
                        dbEntry.SheriffDirected = rdLstSheriffDirected.SelectedIndex == 0;
                        dbEntry.WritNumber = txtWritNumber.Text;
                        dbEntry.NameOfSherrif = txtNameOfSherrif.Text;
                        dbEntry.AddressOfSheriff = txtAddressOfSheriff.Text;
                        dbEntry.LiensOrSecurityInterests = rdLstLiensOrSecurityInterests.SelectedIndex == 0;
                        dbEntry.LiensOrSecurityInterestsNameOfPerson = txtLiensOrSecurityInterestsNameOfPerson.Text;
                        dbEntry.AmountOfMoneyOwed = txtAmountOfMoneyOwed.Text;
                        dbEntry.RegitrationNumber = txtRegitrationNumber.Text;
                        dbEntry.FinancialStatementsRegistered = rdLstFinancialStatementsRegistered.SelectedIndex == 0;
                        dbEntry.FinancialStatementsRegitrationNumber = txtFinancialStatementsRegitrationNumber.Text;
                        dbEntry.UndischargedBankrupt = rdLstUndischargedBankrupt.SelectedIndex == 0;
                        dbEntry.DetailsOfBankruptcy = txtDetailsOfBankruptcy.Text;
                        dbEntry.EntryDate = DateTime.Today;
                        dbEntry.EntryIP = CommonFunctions.GetIpAddress();
                        dbEntry.IsActive = true;
                        dbEntry.EntryId = UserId;
                    }
                    else
                    {
                        var entry = new FinancialInformation
                        {
                            Id = id,
                            PdfFormTemplateId = Guid.Parse(CurrentId),
                            UserId = UserId,
                            CourtOrTribunalOrder = rdLstCourtOrTribunalOrder.SelectedIndex == 0,
                            CourtFileNumber = txtCourtFileNumber.Text,
                            NameOfCourt = txtNameOfCourt.Text,
                            DateOfCourtOrderDay =
                                string.IsNullOrEmpty(txtDateCourtOrder.Text)
                                    ? 0
                                    : DateTime.ParseExact(txtDateCourtOrder.Text, "MM/dd/yyyy", null).Day,
                            DateOfCourtOrderMonth =
                                string.IsNullOrEmpty(txtDateCourtOrder.Text)
                                    ? 0
                                    : DateTime.ParseExact(txtDateCourtOrder.Text, "MM/dd/yyyy", null).Month,
                            DateOfCourtOrderYear =
                                string.IsNullOrEmpty(txtDateCourtOrder.Text)
                                    ? 0
                                    : DateTime.ParseExact(txtDateCourtOrder.Text, "MM/dd/yyyy", null).Year,
                            NameOfPersonWhoSuedYou = txtNameOfPersonWhoSuedYou.Text,
                            AddressCourtTribunal = txtAddressCourtTribunal.Text,
                            SheriffDirected = rdLstSheriffDirected.SelectedIndex == 0,
                            WritNumber = txtWritNumber.Text,
                            NameOfSherrif = txtNameOfSherrif.Text,
                            AddressOfSheriff = txtAddressOfSheriff.Text,
                            LiensOrSecurityInterests = rdLstLiensOrSecurityInterests.SelectedIndex == 0,
                            LiensOrSecurityInterestsNameOfPerson = txtLiensOrSecurityInterestsNameOfPerson.Text,
                            AmountOfMoneyOwed = txtAmountOfMoneyOwed.Text,
                            RegitrationNumber = txtRegitrationNumber.Text,
                            FinancialStatementsRegistered = rdLstFinancialStatementsRegistered.SelectedIndex == 0,
                            FinancialStatementsRegitrationNumber = txtFinancialStatementsRegitrationNumber.Text,
                            UndischargedBankrupt = rdLstUndischargedBankrupt.SelectedIndex == 0,
                            DetailsOfBankruptcy = txtDetailsOfBankruptcy.Text,
                            EntryDate = DateTime.Today,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            IsActive = true,
                            EntryId = UserId,
                        };

                        dataContext.FinancialInformations.Add(entry);
                    }

                    dataContext.SaveChanges();

                    returnMessage = "Data submitted successfully!";
                     
                }
            }
            catch (DbEntityValidationException ex)
            {
                returnMessage =
                    ex.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors)
                        .Aggregate(returnMessage,
                            (current, ve) => current + (ve.PropertyName + " Error Msg:" + ve.ErrorMessage));
            }

            return returnMessage;
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

            Response.Redirect("../Secured/FormCompleted.aspx");
        }
    }
}