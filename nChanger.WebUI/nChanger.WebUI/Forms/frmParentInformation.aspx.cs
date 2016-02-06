using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using nChanger.Core;


namespace nChanger.WebUI.Forms
{
    public partial class FrmParentInformation : AppBasePage 
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
                hypBack.NavigateUrl = "../Forms/frmONPersonalInfo.aspx?id=" + Request.QueryString["id"];
                try
                {
                    var id = Guid.Parse(Request.QueryString["id"]);

                    using (var dataContext = new nChangerCore())
                    {
                        var frmParentInformation =
                            dataContext.ParentInformations.FirstOrDefault(
                                f => f.UserId.Equals(UserId) && f.PdfTemplateId.Equals(id));

                        if (frmParentInformation != null)
                        {
                            btnPreviewPdf.CssClass = string.Empty;
                            btnPreviewPdf.CssClass = "btn btn-sm btn-primary";

                            txtFatherFirstName.Text = frmParentInformation.FatherFirstName;	
                            txtFatherMiddleName.Text = frmParentInformation.FatherMiddleName;
                            txtFatherLastName.Text = frmParentInformation.FatherLastName;
                            txtFatherLastNameOther.Text = frmParentInformation.FatherOtherLastName;
                            txtMotherFirstName.Text = frmParentInformation.MotherFirstName;
                            txtMotherMiddleName.Text = frmParentInformation.MotherMiddleName;
                            txtMotherLastNameBorn.Text = frmParentInformation.MotherLastNameWhenBorn;
                            txtMotherLastNamePresent.Text = frmParentInformation.MotherLastNamePresent;
                            txtMotherLastNameOther.Text = frmParentInformation.MotherLastNameOther;
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

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            divMsg.InnerText = Submit();
            Response.Redirect("frmNameChangeInformation.aspx?id=" + Request.QueryString["id"]);
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
                        dataContext.ParentInformations.FirstOrDefault(
                            f => f.UserId.Equals(UserId) && f.PdfTemplateId.Equals(id));


                    if (dbEntry != null)
                    {
                         dbEntry.FatherFirstName = txtFatherFirstName.Text;
                         dbEntry.FatherMiddleName = txtFatherMiddleName.Text;
                         dbEntry.FatherLastName = txtFatherLastName.Text;
                         dbEntry.FatherOtherLastName = txtFatherLastNameOther.Text;
                         dbEntry.MotherFirstName = txtMotherFirstName.Text;
                         dbEntry.MotherMiddleName = txtMotherMiddleName.Text;
                         dbEntry.MotherLastNameWhenBorn = txtMotherLastNameBorn.Text;
                         dbEntry.MotherLastNamePresent = txtMotherLastNamePresent.Text;
                         dbEntry.MotherLastNameOther = txtMotherLastNameOther.Text;
                         dbEntry.EntryDate = DateTime.Today;
                         dbEntry.EntryIP = CommonFunctions.GetIpAddress();
                         dbEntry.IsActive = true;
                         dbEntry.EntryId = UserId;

                         dataContext.Entry(dbEntry).State = EntityState.Modified;
                    }
                    else
                    {
                        var entry = new ParentInformation()
                        {
                            Id = Guid.NewGuid(),
                            PdfTemplateId = id,
                            UserId = UserId,
                            FatherFirstName = txtFatherFirstName.Text,
                            FatherMiddleName = txtFatherMiddleName.Text,
                            FatherLastName = txtFatherLastName.Text,
                            FatherOtherLastName = txtFatherLastNameOther.Text,
                            MotherFirstName = txtMotherFirstName.Text,
                            MotherMiddleName = txtMotherMiddleName.Text,
                            MotherLastNameWhenBorn = txtMotherLastNameBorn.Text,
                            MotherLastNamePresent = txtMotherLastNamePresent.Text,
                            MotherLastNameOther = txtMotherLastNameOther.Text,
                            EntryDate = DateTime.Today,
                            EntryIP = CommonFunctions.GetIpAddress(),
                            IsActive = true,
                            EntryId = UserId
                        };

                         dataContext.ParentInformations.Add(entry);
 
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
            var id = Guid.Parse(Request.QueryString["id"]);
            using (var dataContext = new nChangerCore())
            {
                var frmOn =
                            dataContext.ParentInformations.FirstOrDefault(
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