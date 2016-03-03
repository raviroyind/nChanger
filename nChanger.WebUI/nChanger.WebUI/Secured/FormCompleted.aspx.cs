using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using nameChanger.WebUI;
using nChanger.Core;

namespace nChanger.WebUI.Secured
{
    public partial class FormCompleted : AppBasePage
    {
        private FileInfo _fileInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Display();
        }

        private void Display()
        {
            var id = Guid.Parse(CurrentId);
            var dataContext =new nChangerDb();

            _fileInfo = new FileInfo(PdfInjector.FillForm(id, UserId));
              
            var template = dataContext.PdfFormTemplates.Find(id);


            if (template != null)
            {
                hypFile.NavigateUrl = "../Output/"+ Path.GetFileName(_fileInfo.Name);
                hypFile.Text = Path.GetFileName(_fileInfo.Name);
            }

            UpdateGeneratedPdf(_fileInfo);
        }

        protected void lnkSend_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmailId.Text))
            {
                var id = Guid.Parse(CurrentId);
                _fileInfo = new FileInfo(PdfInjector.FillForm(id, UserId));
                var attachement = new Attachment(_fileInfo.FullName);
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
            var id = Guid.Parse(CurrentId);
            _fileInfo = new FileInfo(PdfInjector.FillForm(id, UserId));
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + _fileInfo.Name);
            Response.AddHeader("Content-Length", _fileInfo.Length.ToString());
            Response.ContentType = "text/plain";
            Response.Flush();
            Response.TransmitFile(_fileInfo.FullName);
            Response.End();
        }


        private void UpdateGeneratedPdf(FileInfo fileInfo)
        {
            var id = Guid.Parse(CurrentId);
            using (var dataContext=new nChangerDb())
            {
                var dbEntry = dataContext.GeneratedPdfs.SingleOrDefault(g => g.PdfFormTemplateId.Equals(id) && g.UserId.Equals(UserId));
                if (dbEntry != null)
                {
                    dataContext.GeneratedPdfs.Remove(dbEntry);
                    dataContext.SaveChanges();
                }

                dataContext.GeneratedPdfs.AddOrUpdate(new GeneratedPdf
                {
                    Id=Guid.NewGuid(),
                    PdfFormTemplateId = id,
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