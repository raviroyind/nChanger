using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using nChanger.Core;

namespace nChanger.WebUI.Forms
{
    public partial class FrmCriminalOffenceInformation : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var loginName = (HtmlAnchor)Master.FindControl("ancLoginName");
                loginName.InnerText = "Welcome " + Convert.ToString(Session["USR_NAME"]);

                var anHome = (HtmlAnchor)Master.FindControl("anHome");
                anHome.HRef = "~/Secured/Home.aspx";

                Display();
            }
        }

        private void Display()
        {
            if (Request.QueryString["id"] != null)
            {
                hypBack.NavigateUrl = "../Forms/frmNameChangeInformation.aspx?id=" + Request.QueryString["id"];
                try
                {
                    var id = Guid.Parse(Request.QueryString["id"]);

                    using (var dataContext = new nChangerDb())
                    {
                        var frmCriminalOffenceInformation =
                            dataContext.CriminalOffenceInformations.FirstOrDefault(
                                f => f.UserId.Equals(UserId) && f.PdfTemplateId.Equals(id));

                        if (frmCriminalOffenceInformation != null)
                        {
                            btnPreviewPdf.CssClass = string.Empty;
                            btnPreviewPdf.CssClass = "btn btn-sm btn-primary";

                            if (frmCriminalOffenceInformation.OutstandingCourtProceedings)
                                rdLstOutstandingCourtProceedings.SelectedIndex = 0;
                            else
                                rdLstOutstandingCourtProceedings.SelectedIndex = 1;

                            txtCourtFileNumber.Text = frmCriminalOffenceInformation.CourtFileNumber;
                            txtCourtName.Text = frmCriminalOffenceInformation.CourtName;
                            txtCourtAddress.Text = frmCriminalOffenceInformation.CourtAddress;
                            txtDescribeProceedings.Text = frmCriminalOffenceInformation.DescribeProceedings;

                            if (frmCriminalOffenceInformation.OutstandingEnforcementOrders)
                                rdLstOutstandingEnforcementOrders.SelectedIndex = 0;
                            else
                                rdLstOutstandingEnforcementOrders.SelectedIndex = 1;


                           txtDetailsOfOutstandingEnforcementOrders.Text = frmCriminalOffenceInformation.DetailsOfOutstandingEnforcementOrders;

                            if (frmCriminalOffenceInformation.EverConvictedOfCriminalOffence)
                                rdLstEverConvictedOfCriminalOffence.SelectedIndex = 0;
                            else
                                rdLstEverConvictedOfCriminalOffence.SelectedIndex = 1;


                           txtDetailsOfCriminalOffence.Text = frmCriminalOffenceInformation.DetailsOfCriminalOffence;

                            if (frmCriminalOffenceInformation.FoundGuiltyDischarged)
                                rdLstFoundGuiltyDischarged.SelectedIndex = 0;
                            else
                                rdLstFoundGuiltyDischarged.SelectedIndex = 1;

                            
                           txtFoundGuiltyDetailsOfOffence.Text = frmCriminalOffenceInformation.FoundGuiltyDetailsOfOffence;
                           
                            if(frmCriminalOffenceInformation.AdultSentenceImposed)
                                rdLstAdultSentenceImposed.SelectedIndex = 0;
                            else
                                rdLstAdultSentenceImposed.SelectedIndex = 1;


                            txtDescribeAdultSentence.Text = frmCriminalOffenceInformation.DescribeAdultSentence;

                            if(frmCriminalOffenceInformation.PendingCharges)
                                rdLstPendingCharges.SelectedIndex = 0;
                            else
                                rdLstPendingCharges.SelectedIndex = 1;

                            txtPendingChargesCourtNumber.Text = frmCriminalOffenceInformation.PendingChargesCourtNumber;

                            txtPendingChargesCourtName.Text = frmCriminalOffenceInformation.PendingChargesCourtName;
                            txtPendingChargesCourtAddress.Text = frmCriminalOffenceInformation.PendingChargesCourtAddress;
                            txtPendingChargesDescribe.Text =  frmCriminalOffenceInformation.PendingChargesDescribe;
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
        }

        private string Submit()
        {
            var id = Guid.Parse(Request.QueryString["id"]);
            var returnMessage = string.Empty;
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry =
                        dataContext.CriminalOffenceInformations.FirstOrDefault(
                            f => f.UserId.Equals(UserId) && f.PdfTemplateId.Equals(id));


                    if (dbEntry != null)
                    {
                        dbEntry.UserId = UserId;
                        dbEntry.OutstandingCourtProceedings = rdLstOutstandingCourtProceedings.SelectedIndex == 0;
                        dbEntry.CourtFileNumber = txtCourtFileNumber.Text;
                        dbEntry.CourtName = txtCourtName.Text;
                        dbEntry.CourtAddress = txtCourtAddress.Text;
                        dbEntry.DescribeProceedings = txtDescribeProceedings.Text;
                        dbEntry.OutstandingEnforcementOrders = rdLstOutstandingEnforcementOrders.SelectedIndex == 0;
                        dbEntry.DetailsOfOutstandingEnforcementOrders = txtDetailsOfOutstandingEnforcementOrders.Text;
                        dbEntry.EverConvictedOfCriminalOffence = rdLstEverConvictedOfCriminalOffence.SelectedIndex == 0;
                        dbEntry.DetailsOfCriminalOffence = txtDetailsOfCriminalOffence.Text;
                        dbEntry.FoundGuiltyDischarged =  rdLstFoundGuiltyDischarged.SelectedIndex == 0;
                        dbEntry.FoundGuiltyDetailsOfOffence = txtFoundGuiltyDetailsOfOffence.Text;
                        dbEntry.AdultSentenceImposed = rdLstAdultSentenceImposed.SelectedIndex == 0;
                        dbEntry.DescribeAdultSentence = txtDescribeAdultSentence.Text;
                        dbEntry.PendingCharges = rdLstPendingCharges.SelectedIndex == 0;
                        dbEntry.PendingChargesCourtNumber = txtPendingChargesCourtNumber.Text;
                        dbEntry.PendingChargesCourtName =  txtPendingChargesCourtName.Text;
                        dbEntry.PendingChargesCourtAddress =  txtPendingChargesCourtAddress.Text;
                        dbEntry.PendingChargesDescribe = txtPendingChargesDescribe.Text;
                        dbEntry.EntryDate = DateTime.Today;
                        dbEntry.EntryIP = CommonFunctions.GetIpAddress();
                        dbEntry.IsActive = true;
                        dbEntry.EntryId = UserId;
                    }
                    else
                    {
                        var entry = new CriminalOffenceInformation
                        {
                            Id = Guid.NewGuid(),
                            PdfTemplateId = id,
                            UserId = UserId,
                            OutstandingCourtProceedings =  rdLstOutstandingCourtProceedings.SelectedIndex == 0,
                            CourtFileNumber = txtCourtFileNumber.Text ,
                            CourtName = txtCourtName.Text,
                            CourtAddress = txtCourtAddress.Text,
                            DescribeProceedings = txtDescribeProceedings.Text,
                            OutstandingEnforcementOrders = rdLstOutstandingEnforcementOrders.SelectedIndex == 0,
                            DetailsOfOutstandingEnforcementOrders = txtDetailsOfOutstandingEnforcementOrders.Text,
                            EverConvictedOfCriminalOffence = rdLstEverConvictedOfCriminalOffence.SelectedIndex == 0,
                            DetailsOfCriminalOffence = txtDetailsOfCriminalOffence.Text,
                            FoundGuiltyDischarged =  rdLstFoundGuiltyDischarged.SelectedIndex == 0,
                            FoundGuiltyDetailsOfOffence = txtFoundGuiltyDetailsOfOffence.Text,
                            AdultSentenceImposed = rdLstAdultSentenceImposed.SelectedIndex == 0,
                            DescribeAdultSentence = txtDescribeAdultSentence.Text,
                            PendingCharges = rdLstPendingCharges.SelectedIndex == 0,
                            PendingChargesCourtNumber = txtPendingChargesCourtNumber.Text,
                            PendingChargesCourtName =  txtPendingChargesCourtName.Text,
                            PendingChargesCourtAddress =  txtPendingChargesCourtAddress.Text,
                            PendingChargesDescribe = txtPendingChargesDescribe.Text,
                            EntryDate = DateTime.Today,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            IsActive = true,
                            EntryId = UserId,
                        };

                        dataContext.CriminalOffenceInformations.Add(entry);
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

        protected  void btnSubmit_OnClick(object sender, EventArgs e)
        {
            divMsg.InnerText = Submit();
            Response.Redirect("frmFinancialInformation.aspx?id=" + Request.QueryString["id"]);
        }

        protected void btnPreviewPdf_OnClick(object sender, EventArgs e)
        {
            
            var id = Guid.Parse(Request.QueryString["id"]);
            using (var dataContext = new nChangerDb())
            {
                var frmOn =
                            dataContext.CriminalOffenceInformations.FirstOrDefault(
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