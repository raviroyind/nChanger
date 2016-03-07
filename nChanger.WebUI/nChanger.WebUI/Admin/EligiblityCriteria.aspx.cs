using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Admin
{
    public partial class EligiblityCriteria : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var dataContext = new nChangerDb())
                {
                    var eligibilityCriteria = dataContext.EligibilityCriterias.FirstOrDefault();
                    if (eligibilityCriteria != null)
                    {
                        hypEdit.Text = "<i class=\"edit icon large orange\"></i> Edit Criteria";
                        hypEdit.CssClass = "ui right floated button";
                        hypEdit.NavigateUrl = "DefineEligiblityCriteria.aspx?id=" + eligibilityCriteria.Id;
                        lblCriteriaHeading.Text = eligibilityCriteria.CriteriaHeading;
                        divCriteria.InnerHtml = eligibilityCriteria.Criteria;
                    }
                    else
                    {
                        hypEdit.Text = "<i class=\"plus icon large green\"></i> Add Criteria";
                        hypEdit.CssClass = "ui right floated button";
                        hypEdit.NavigateUrl = "DefineEligiblityCriteria.aspx";
                    }
                }
            }
        }
    }
}