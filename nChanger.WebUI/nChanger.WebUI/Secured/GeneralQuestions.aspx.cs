using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Secured
{
    public partial class GeneralQuestions : AppBasePage
    {
        private const string TableNames = "CriminalOffenceInformation,FinancialInformation,NameChangeInformation,ParentInformation,PersonalInformation";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                    GetGeneralQuestions(Guid.Parse(Request.QueryString["id"]));
            }
        }

        private void GetGeneralQuestions(Guid id)
        {
            using (var dataContext=new nChangerDb())
            {
               var pdfTemplate = dataContext.PdfFormTemplates.Find(id);
               var fieldMappings = from p in dataContext.FieldMappings
                                   where ! p.TableName.Contains(TableNames) 
                                   && ! p.TableName.Equals("")
                                    && p.PdfFormTemplateId.Equals(id)
                                   select p;


            }
        }
    }
}