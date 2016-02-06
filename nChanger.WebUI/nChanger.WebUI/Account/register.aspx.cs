using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI.Account
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdowns();
            }
        }
        private void BindDropdowns()
        {

            using (var dataContext = new nChangerCore())
            {
                ddlCountry.DataSource = dataContext.Countries.ToList();
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("--SELECT--", "SEL"));

            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue.Equals("US") || ddlCountry.SelectedValue.Equals("CA"))
            {
                var country = ddlCountry.SelectedValue;
                using (var dataContext = new nChangerCore())
                {
                    ddlState.DataSource = dataContext.States.Where(s => s.Country.Equals(country)).ToList();
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();

                    ddlState.Items.Insert(0, new ListItem("--SELECT--", "SEL"));
                    ddlState.CssClass = "ui fluid dropdown selection";

                    divDropState.Style.Add(HtmlTextWriterStyle.Display, "block");
                    divStateText.Style.Add(HtmlTextWriterStyle.Display, "none");

                }
            }
            else
            {
                divDropState.Style.Add(HtmlTextWriterStyle.Display, "none");
                divStateText.Style.Add(HtmlTextWriterStyle.Display, "block");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void cvEMail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (CommonFunctions.CheckIfUserExists(args.Value))
            {
                args.IsValid = false;
                lblSpan.Text = "invalid...";
            }
        }
    }
}