using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using nChanger.Core;
 
using nChanger.WebUI.Navigation;

namespace nChanger.WebUI.Secured
{
    public partial class Dashboard :AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindProvince();
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
                    Response.Redirect("packageSelection.aspx?id="+pdfTemplateId.ToString());
                }
                
            }
            
        }
    }
}