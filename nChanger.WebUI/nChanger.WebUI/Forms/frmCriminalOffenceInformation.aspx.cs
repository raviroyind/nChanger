using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;
using System.IO;
using nameChanger.WebUI;

namespace nChanger.WebUI.Forms
{
    public partial class frmCriminalOffenceInformation : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormIndex = 4;
                Display();
            }
        }

        private void Display()
        { 
                try
                {
                    using (var dataContext = new nChangerDb())
                    {
                        var id = Guid.Parse(RecordId);

                        var criminalOffenceInformation = dataContext.CriminalOffenceInformations.Find(id);

                        if (criminalOffenceInformation != null)
                        {
                            hypBack.NavigateUrl = "../Forms/frmNameChangeInformation.aspx";
                         
                            rdLstOutstandingCourtProceedings.SelectedIndex = criminalOffenceInformation.OutstandingCourtProceedings ? 0 : 1;

                            txtCourtFileNumber.Text = criminalOffenceInformation.CourtFileNumber;
                            txtCourtName.Text = criminalOffenceInformation.CourtName;
                            txtCourtAddress.Text = criminalOffenceInformation.CourtAddress;
                            txtDescribeProceedings.Text = criminalOffenceInformation.DescribeProceedings;

                            rdLstOutstandingEnforcementOrders.SelectedIndex = criminalOffenceInformation.OutstandingEnforcementOrders ? 0 : 1;


                            txtDetailsOfOutstandingEnforcementOrders.Text = criminalOffenceInformation.DetailsOfOutstandingEnforcementOrders;

                            rdLstEverConvictedOfCriminalOffence.SelectedIndex = criminalOffenceInformation.EverConvictedOfCriminalOffence ? 0 : 1;


                            txtDetailsOfCriminalOffence.Text = criminalOffenceInformation.DetailsOfCriminalOffence;

                            rdLstFoundGuiltyDischarged.SelectedIndex = criminalOffenceInformation.FoundGuiltyDischarged ? 0 : 1;


                            txtFoundGuiltyDetailsOfOffence.Text = criminalOffenceInformation.FoundGuiltyDetailsOfOffence;

                            rdLstAdultSentenceImposed.SelectedIndex = criminalOffenceInformation.AdultSentenceImposed ? 0 : 1;


                            txtDescribeAdultSentence.Text = criminalOffenceInformation.DescribeAdultSentence;

                            rdLstPendingCharges.SelectedIndex = criminalOffenceInformation.PendingCharges ? 0 : 1;

                            txtPendingChargesCourtNumber.Text = criminalOffenceInformation.PendingChargesCourtNumber;

                            txtPendingChargesCourtName.Text = criminalOffenceInformation.PendingChargesCourtName;
                            txtPendingChargesCourtAddress.Text = criminalOffenceInformation.PendingChargesCourtAddress;
                            txtPendingChargesDescribe.Text = criminalOffenceInformation.PendingChargesDescribe;
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
                    var dbEntry = dataContext.CriminalOffenceInformations.Find(id);
                    
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
                        dbEntry.FoundGuiltyDischarged = rdLstFoundGuiltyDischarged.SelectedIndex == 0;
                        dbEntry.FoundGuiltyDetailsOfOffence = txtFoundGuiltyDetailsOfOffence.Text;
                        dbEntry.AdultSentenceImposed = rdLstAdultSentenceImposed.SelectedIndex == 0;
                        dbEntry.DescribeAdultSentence = txtDescribeAdultSentence.Text;
                        dbEntry.PendingCharges = rdLstPendingCharges.SelectedIndex == 0;
                        dbEntry.PendingChargesCourtNumber = txtPendingChargesCourtNumber.Text;
                        dbEntry.PendingChargesCourtName = txtPendingChargesCourtName.Text;
                        dbEntry.PendingChargesCourtAddress = txtPendingChargesCourtAddress.Text;
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
                            Id = id,
                            PdfFormTemplateId = Guid.Parse(CurrentId),
                            UserId = UserId,
                            OutstandingCourtProceedings = rdLstOutstandingCourtProceedings.SelectedIndex == 0,
                            CourtFileNumber = txtCourtFileNumber.Text,
                            CourtName = txtCourtName.Text,
                            CourtAddress = txtCourtAddress.Text,
                            DescribeProceedings = txtDescribeProceedings.Text,
                            OutstandingEnforcementOrders = rdLstOutstandingEnforcementOrders.SelectedIndex == 0,
                            DetailsOfOutstandingEnforcementOrders = txtDetailsOfOutstandingEnforcementOrders.Text,
                            EverConvictedOfCriminalOffence = rdLstEverConvictedOfCriminalOffence.SelectedIndex == 0,
                            DetailsOfCriminalOffence = txtDetailsOfCriminalOffence.Text,
                            FoundGuiltyDischarged = rdLstFoundGuiltyDischarged.SelectedIndex == 0,
                            FoundGuiltyDetailsOfOffence = txtFoundGuiltyDetailsOfOffence.Text,
                            AdultSentenceImposed = rdLstAdultSentenceImposed.SelectedIndex == 0,
                            DescribeAdultSentence = txtDescribeAdultSentence.Text,
                            PendingCharges = rdLstPendingCharges.SelectedIndex == 0,
                            PendingChargesCourtNumber = txtPendingChargesCourtNumber.Text,
                            PendingChargesCourtName = txtPendingChargesCourtName.Text,
                            PendingChargesCourtAddress = txtPendingChargesCourtAddress.Text,
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