using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using nChanger.Core;

namespace nChanger.WebUI.Forms
{
    public partial class FrmFinancialInformation : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var loginName = (HtmlAnchor) Master.FindControl("ancLoginName");
                loginName.InnerText = "Welcome " + Convert.ToString(Session["USR_NAME"]);

                var anHome = (HtmlAnchor) Master.FindControl("anHome");
                anHome.HRef = "~/Secured/Home.aspx";

                Display();
            }
        }

        private void Display()
        {
            if (Request.QueryString["id"] != null)
            {
                hypBack.NavigateUrl = "../Forms/frmCriminalOffenceInformation.aspx?id=" + Request.QueryString["id"];
                try
                {
                    var id = Guid.Parse(Request.QueryString["id"]);

                    using (var dataContext = new nChangerCore())
                    {
                        var frmFinancialInformation =
                            dataContext.FinancialInformations.FirstOrDefault(
                                f => f.UserId.Equals(UserId) && f.PdfTemplateId.Equals(id));

                        if (frmFinancialInformation != null)
                        {
                            rdLstCourtOrTribunalOrder.SelectedIndex = frmFinancialInformation.CourtOrTribunalOrder
                                ? 0
                                : 1;

                            txtCourtFileNumber.Text = frmFinancialInformation.CourtFileNumber;
                            txtNameOfCourt.Text = frmFinancialInformation.NameOfCourt;

                            if (frmFinancialInformation.DateOfCourtOrderDay > 0 &&
                                frmFinancialInformation.DateOfCourtOrderMonth > 0 &&
                                frmFinancialInformation.DateOfCourtOrderYear > 0)
                            {
                                DateTime dt = new DateTime(frmFinancialInformation.DateOfCourtOrderYear.Value,
                                    frmFinancialInformation.DateOfCourtOrderMonth.Value,
                                    frmFinancialInformation.DateOfCourtOrderDay.Value);
                                txtDateCourtOrder.Text = dt.ToString("MM/dd/yyyy");
                            }

                            txtNameOfPersonWhoSuedYou.Text = frmFinancialInformation.NameOfPersonWhoSuedYou;
                            txtAddressCourtTribunal.Text = frmFinancialInformation.AddressCourtTribunal;

                            rdLstSheriffDirected.SelectedIndex = frmFinancialInformation.SheriffDirected ? 0 : 1;
                            txtWritNumber.Text = frmFinancialInformation.WritNumber;
                            txtNameOfSherrif.Text = frmFinancialInformation.NameOfSherrif;
                            txtAddressOfSheriff.Text = frmFinancialInformation.AddressOfSheriff;

                            rdLstLiensOrSecurityInterests.SelectedIndex =
                                frmFinancialInformation.LiensOrSecurityInterests ? 0 : 1;
                            txtLiensOrSecurityInterestsNameOfPerson.Text =
                                frmFinancialInformation.LiensOrSecurityInterestsNameOfPerson;
                            txtAmountOfMoneyOwed.Text = frmFinancialInformation.AmountOfMoneyOwed;
                            txtRegitrationNumber.Text = frmFinancialInformation.RegitrationNumber;

                            rdLstFinancialStatementsRegistered.SelectedIndex =
                                frmFinancialInformation.FinancialStatementsRegistered ? 0 : 1;
                            txtFinancialStatementsRegitrationNumber.Text =
                                frmFinancialInformation.FinancialStatementsRegitrationNumber;

                            rdLstUndischargedBankrupt.SelectedIndex = frmFinancialInformation.UndischargedBankrupt
                                ? 0
                                : 1;
                            txtDetailsOfBankruptcy.Text = frmFinancialInformation.DetailsOfBankruptcy;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private string Submit()
        {
            var id = Guid.Parse(Request.QueryString["id"]);
            var returnMessage = string.Empty;
            try
            {
                using (var dataContext = new nChangerCore())
                {
                    var dbEntry =
                        dataContext.FinancialInformations.FirstOrDefault(
                            f => f.UserId.Equals(UserId) && f.PdfTemplateId.Equals(id));


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
                            Id = Guid.NewGuid(),
                            PdfTemplateId = id,
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
                    btnPreviewPdf.CssClass = string.Empty;
                    btnPreviewPdf.CssClass = "btn btn-sm btn-primary";
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
        }

        protected void btnPreviewPdf_OnClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(Request.QueryString["id"]);
            using (var dataContext = new nChangerCore())
            {
                var frmOn =
                            dataContext.FinancialInformations.FirstOrDefault(
                                f => f.UserId.Equals(UserId) && f.PdfTemplateId.Equals(id));

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