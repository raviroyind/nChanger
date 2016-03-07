using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using nameChanger.WebUI;
using nChanger.Core;

namespace nChanger.WebUI.Secured
{
    public partial class FormCompleted : AppBasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Display();
        }

        private void Display()
        {
            var id = Guid.Parse(RecordId);
            var pdfFormTemplateid = Guid.Parse(CurrentId);
            var dataContext =new nChangerDb();

            var  fileInfo = new FileInfo(PdfInjector.FillForm(pdfFormTemplateid,id, UserId));

            hidFile.Value = fileInfo.FullName; 

            var template = dataContext.PdfFormTemplates.Find(pdfFormTemplateid);


            if (template != null)
            {
                hypFile.NavigateUrl = "../Output/"+ Path.GetFileName(fileInfo.Name);
                hypFile.Text = Path.GetFileName(fileInfo.Name);
            }

            UpdateGeneratedPdf(fileInfo);
        }

        protected void lnkSend_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmailId.Text))
            {
                var fInfo = new FileInfo(hidFile.Value);
                var attachement = new Attachment(fInfo.FullName);
                var streamReader = new StreamReader(Server.MapPath(@"~/MailTemplates/sendDocument.html"));
                var htmlContent = streamReader.ReadToEnd();
                var mailBody = htmlContent.Replace("[USER]", UserName);
                streamReader.Close();

                if (CommonFunctions.SendMail(txtEmailId.Text, "[Name Changer] Document - " + hypFile.Text, true,
                    mailBody, true, attachement))
                {
                    lblMsg.Text = "File " + hypFile.Text + " emailed successfully to "+txtEmailId.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                    txtEmailId.Text = string.Empty;
                }

            }
        }

        protected void lnkDownload_OnClick(object sender, EventArgs e)
        { 
            var fInfo = new FileInfo(hidFile.Value);
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fInfo.Name);
            Response.AddHeader("Content-Length", fInfo.Length.ToString());
            Response.ContentType = "text/plain";
            Response.Flush();
            Response.TransmitFile(fInfo.FullName);
            Response.End();
        }


        private void UpdateGeneratedPdf(FileInfo fileInfo)
        {
            var id = Guid.Parse(RecordId);
            var pdfFormTemplateid = Guid.Parse(CurrentId);
            using (var dataContext=new nChangerDb())
            {
                var dbEntry = dataContext.GeneratedPdfs.Find(id);
                if (dbEntry != null)
                {
                    dataContext.GeneratedPdfs.Remove(dbEntry);
                    dataContext.SaveChanges();
                }

                dataContext.GeneratedPdfs.AddOrUpdate(new GeneratedPdf
                {
                    Id=id,
                    PdfFormTemplateId = pdfFormTemplateid,
                    UserId = UserId,
                    CompletedPdf ="../Output/"+Path.GetFileName(fileInfo.Name),
                    EntryDate = DateTime.Now,
                    EntryIP = CommonFunctions.GetIpAddress(),
                    EntryId= UserId,
                    IsActive=true
                });

                dataContext.SaveChanges();
            }
        }
    }
}