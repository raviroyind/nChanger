using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Secured
{
    public partial class ConfirmEligibility : AppBasePage 
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
                        lblCriteriaHeading.Text = eligibilityCriteria.CriteriaHeading;
                        divCriteria.InnerHtml = eligibilityCriteria.Criteria;
                    }
                }
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    dataContext.Database.ExecuteSqlCommand(
                        "UPDATE Users SET IsEligibilityConfirmed='1' WHERE UserId='" + UserId + "'");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                Response.Redirect("../Secured/dashboard.aspx");
            }
         
        }
    }
}