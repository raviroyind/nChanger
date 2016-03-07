using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using nameChanger.WebUI;
using nChanger.Core;
 
using nChanger.WebUI.Navigation;

namespace nChanger.WebUI.Secured
{
    public partial class Dashboard :AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProvince();
                BindForms();
            }
        }

        private void BindForms()
        {
           
            using (var dataContext = new nChangerDb())
            {
                var lst =
                    dataContext.GeneratedPdfs.Where(
                        u => u.UserId.Equals(UserId))
                        .ToList();

                if (lst.Count>0)
                {
                    gvTemplate.DataSource = lst;
                    gvTemplate.DataBind();
                }
            }
        }

        private void BindProvince()
        {
            using (var dataContext = new nChangerDb())
            {
                BindDropdownList(ddlProvince, dataContext.Provinces.ToList(), "Id", "ProvinceName");
            }
        }
         
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex != 0 && ddlCategory.SelectedIndex != -1)
            {
                using (var dataContext = new nChangerDb())
                {
                    var id = Guid.Parse(ddlCategory.SelectedValue);
                    gvForms.DataSource =
                        dataContext.PdfFormTemplates.Where(p => p.ProvinceCategoryId.Equals(id))
                            .OrderBy(p => p.TemplateName)
                            .ToList();

                    gvForms.DataBind();

                    lblAviliableForms.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "$('.ui.fluid.search.selection.dropdown').dropdown();", true);
        }

        protected void ddlProvince_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvince.SelectedIndex == 0)
            {
                ddlCategory.Items.Clear();
                return;
            }
            using (var dataContext = new nChangerDb())
            {
                var id = Guid.Parse(ddlProvince.SelectedValue);
                BindDropdownList(ddlCategory, dataContext.ProvinceCategories.Where(p => p.ProvinceId.Equals(id)).ToList(), "Id", "Category");
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "$('.ui.fluid.search.selection.dropdown').dropdown();", true);
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            var linkButton = (LinkButton) sender;
             
            var pdfTemplateId = Guid.Parse(linkButton.CommandArgument);

            using (var dataContext = new nChangerDb())
            {
                var pdfTemplate = dataContext.PdfFormTemplates.Find(pdfTemplateId);

                if (pdfTemplate != null)
                {
                    Sections=new List<FrmSection>();
                    foreach (var form in pdfTemplate.FormInfoes.OrderBy(f => f.FormSection.FormOrder))
                    {
                        var path = form.FormSection.FormPath;
                        Sections.Add(new FrmSection
                        {
                            FrmGuid= form.FormSection.Id,
                            TableName=form.FormSection.TableName,
                            AspxPath=form.FormSection.FormPath,
                            DisplayOrder= form.FormSection.FormOrder
                        });
                    }

                    CurrentId = pdfTemplateId.ToString();
                    RecordId = Guid.NewGuid().ToString();
                    Response.Redirect("packageSelection.aspx?id="+pdfTemplateId.ToString());
                }
                
            }
            
        }

        protected void lnkViewForm_OnClick(object sender, EventArgs e)
        {
            var linkButton = (LinkButton)sender;

            var recordId = Guid.Parse(linkButton.CommandArgument);
            
            using (var dataContext = new nChangerDb())
            {
                var genPdf = dataContext.GeneratedPdfs.Find(recordId);

                if(genPdf==null)
                    return;

                var pdfTemplateId = genPdf.PdfFormTemplateId;

                var pdfTemplate = dataContext.PdfFormTemplates.Find(pdfTemplateId);

                if (pdfTemplate != null)
                {
                    Sections = new List<FrmSection>();
                    foreach (var form in pdfTemplate.FormInfoes.OrderBy(f => f.FormSection.FormOrder))
                    {
                        var path = form.FormSection.FormPath;
                        Sections.Add(new FrmSection
                        {
                            FrmGuid = form.FormSection.Id,
                            TableName = form.FormSection.TableName,
                            AspxPath = form.FormSection.FormPath,
                            DisplayOrder = form.FormSection.FormOrder
                        });
                    }

                    CurrentId = pdfTemplateId.ToString();
                    RecordId = recordId.ToString();
                    Response.Redirect("../Forms/CustomQuestions.aspx");
                }
            }
        }

        protected void lnkFrm_OnClick(object sender, EventArgs e)
        {
            var lnkBtn = (LinkButton) sender;
            CurrentId = lnkBtn.CommandArgument;
            Response.Redirect("FormCompleted.aspx");
        }


        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            //var lnkBtn = (LinkButton) sender;
            //CurrentId = lnkBtn.CommandArgument;
            //var id = Guid.Parse(CurrentId);

            //using (var dataContext = new nChangerDb())
            //{
            //    dataContext.Database.ExecuteSqlCommand("")
            //}
        }
         
    }
}