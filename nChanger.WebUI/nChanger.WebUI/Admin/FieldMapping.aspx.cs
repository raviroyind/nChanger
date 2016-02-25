using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using nChanger.Core;

namespace nChanger.WebUI.Admin
{
    public partial class PdfFieldMapping : AppBasePage
    {
        #region Variable Declarations...

        readonly List<FieldMapping> _fieldMapping = new List<FieldMapping>();
        private IList _tableList;
        private string _fileName;
        private IQueryable<string> _columns;
        private  ListItem[] _generalQuestions;
        private nChangerDb _dataContext = new nChangerDb();

        #endregion Variable Declarations...

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["id"] != null)
            //    hypBack.NavigateUrl = "ManagePdfTemplate.aspx?id=" + Request.QueryString["id"];

            if (!IsPostBack)
            {



                if (Request.QueryString["id"] != null)
                {
                    #region Add Quetions....

                    using (var dataContext = new nChangerDb())
                    {
                        var id = Guid.Parse(Request.QueryString["id"]);
                        var provinceCategoryId = dataContext.PdfFormTemplates.Find(id).ProvinceCategoryId;
                        var listQuestions = dataContext.DefineQuestions.Where(q => q.ProvinceCategoryId.Equals(provinceCategoryId)).ToList();

                        if (listQuestions.Count > 0)
                        {
                            _generalQuestions=new ListItem[listQuestions.Count];
                            for (var i = 0; i < listQuestions.Count; i++)
                            {
                                _generalQuestions[i] = new ListItem
                                {
                                    Text = "General Questions | " + listQuestions[i].Question,
                                    Value = listQuestions[i].Id.ToString()
                                };
                            }

                        }
                    }

                    #endregion....

                    LoadPdfData();
                    
                }
            }
        }

        private void LoadPdfData()
        {
            try
            {
                using (var dataContext=new nChangerDb())
                {
                    var pdfFormTemplate = dataContext.PdfFormTemplates.Find(Guid.Parse(Request.QueryString["id"]));
                    if (pdfFormTemplate != null)
                    {
                        txtTemplateName.Text = pdfFormTemplate.TemplateName;
                        hypPdf.Text = pdfFormTemplate.PdfFileName;
                        hypPdf.NavigateUrl = @"../Pdf/" + pdfFormTemplate.PdfFileName;
                         
                        if (Convert.ToString(Request.QueryString["active"]).Equals("False"))
                            ReadPdfFields();
                    }
                }
               
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private void ReadPdfFields()
        {
            _dataContext = new nChangerDb();

            var tables = from c in _dataContext.InputFormTables
                where c.IsActive
                select c.TableName; 
             

            if (!string.IsNullOrEmpty(hypPdf.Text))
            {
                //If EF
                var builder = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["nChangerDb"].ConnectionString);
                var cnnStr = builder.ProviderConnectionString;

                var sqlBuilder = new SqlConnectionStringBuilder(cnnStr);


                _columns = from c in _dataContext.InputFormSchemaViews
                           where tables.Contains(c.TABLE_NAME) && c.TABLE_SCHEMA == "dbo" && c.TABLE_CATALOG == sqlBuilder.InitialCatalog && c.COLUMN_NAME != "Id"
                          && c.COLUMN_NAME != "PdfTemplateId" && c.COLUMN_NAME != "IsActive" && c.COLUMN_NAME != "EntryDate" && c.COLUMN_NAME != "EntryIP"
                           && c.COLUMN_NAME != "EntryId" && c.COLUMN_NAME != "UserId" && c.TABLE_NAME != "InputFormSchemaView" && c.TABLE_NAME != "Users"
                           select c.TABLE_NAME + " | " + c.COLUMN_NAME;



                var pdfTemplate = Server.MapPath(@"~/Pdf/" + hypPdf.Text);


                var pdfReader = new PdfReader(pdfTemplate);


                var formFields = pdfReader.AcroFields;

                if (formFields.Fields.Count == 0)
                {
                    lblMessage.Text = "Pdf file \"" + hypPdf.Text + "\" does not contains any fillable fields to map.";
                    divMsg.Style.Add(HtmlTextWriterStyle.Display, "block");
                    return;
                }


                foreach (var field in pdfReader.AcroFields.Fields)
                {

                    var fieldType = formFields.GetFieldType(field.Key);
                    if (fieldType == 1)
                        continue;

                    var fieldPositions = pdfReader.AcroFields.GetFieldPositions(field.Key);
                    if (fieldPositions == null || fieldPositions.Count <= 0) throw new ApplicationException("Error locating field");
                    var fieldPosition = fieldPositions[0];

                    var page = pdfReader.AcroFields.GetFieldPositions(field.Key)[0].page;

                    _fieldMapping.Add(new FieldMapping
                    {
                        Id = Guid.NewGuid(),
                        PdfFormTemplateId = Guid.Parse(Request.QueryString["id"]),
                        PdfPageNumber = page,
                        PdfFieldName = formFields.GetTranslatedFieldName(field.Key),
                        Bottom = Convert.ToDouble(fieldPosition.position.Bottom.ToString("0.00")),
                        Left = Convert.ToDouble(fieldPosition.position.Left.ToString("0.00")),
                        Right = Convert.ToDouble(fieldPosition.position.Right.ToString("0.00")),
                        Top = Convert.ToDouble(fieldPosition.position.Top.ToString("0.00")),
                        IsActive = true,
                        EntryIP = CommonFunctions.GetIpAddress(),
                        EntryDate = DateTime.Today,
                        EntryId = UserId,
                        FieldType = GetFieldType(fieldType),

                    });
                }

                if (_fieldMapping.Count > 0)
                { 
                    BindGrid();
                    Session.Add("LIST", _fieldMapping);
                    //btnSubmit.CssClass = "ui button animated fade fluid";
                    gvFields.Visible = true;
                }
                //else
                    //btnSubmit.CssClass = "ui button animated fade fluid disabled";
            }
        }

        private static string GetFieldType(int fieldType)
        {
            var returnValue = string.Empty;
            switch (fieldType.ToString())
            {
                case "1":
                    returnValue = "Button";
                    break;
                case "2":
                    returnValue = "CheckBox ";
                    break;
                case "3":
                    returnValue = "RadioButton";
                    break;
                case "4":
                    returnValue = "TextField";
                    break;
                case "5":
                    returnValue = "ListBox";
                    break;
                case "6":
                    returnValue = "ComboBox";
                    break;

            }

            return returnValue;
        }

        private void BindGrid()
        {
            ViewState["sortColumn"] = "";
            ViewState["sortDirection"] = "";

            //Start ################ Bellow code is for Initializing Paging ###############                
            TextBox txtPageno1 = (TextBox)ucPaging.FindControl("txtPageNo");

            txtPageno1.Attributes.Add("onkeypress", "return SetPagenoValue('" + txtPageno1.ClientID + "','" + txtPageno1.ClientID + "');");
            //End

            var list = _fieldMapping.OrderBy(f => f.PdfPageNumber).ToList();
            var dtSearch = CommonFunctions.ToDataTable<FieldMapping>(list);

            if (dtSearch != null)
            {
                Session.Add("GetDataTable", dtSearch);
                ucPaging.BindPaging(gvFields, dtSearch, ucPaging.PageNo, "txt",
                    Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                BindBottomPaging(ucPaging, ucPaging1);
            }

        }

        protected void gvFields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddlSqlColumn = (DropDownList)e.Row.FindControl("ddlSQLColumn");
                if (ddlSqlColumn != null)
                {

                    BindDropdownList(ddlSqlColumn, _columns.ToList(), "", "");
                    ddlSqlColumn.Items.AddRange(_generalQuestions);


                    if (Request.QueryString["id"] != null)
                    {
                        var filedId = Convert.ToString(gvFields.DataKeys[e.Row.RowIndex].Values[0]);

                        var field = _dataContext.FieldMappings.Find(Guid.Parse(filedId));

                        if (field != null)
                        {
                            ddlSqlColumn.ClearSelection();

                            var lastOrDefault = ddlSqlColumn.Items.Cast<ListItem>().LastOrDefault(x => x.Value.Contains(field.DbFieldName));
                            if (lastOrDefault != null)
                                lastOrDefault.Selected = true;
                        }
                    }
                }
            }
        }

      

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            var fieldMapping = (List<FieldMapping>)Session["LIST"];
             
            var pdfFormTemplateId = Guid.Parse(Request.QueryString["id"]);

            #region PDF Template..

            var pdfFormTemplate = _dataContext.PdfFormTemplates.Find(pdfFormTemplateId);

            if (pdfFormTemplate != null)
            {
                pdfFormTemplate.TemplateName = txtTemplateName.Text;
                pdfFormTemplate.IsActive = true;
                pdfFormTemplate.EntryDate = DateTime.Today;
                pdfFormTemplate.EntryIP = CommonFunctions.GetIpAddress();
                pdfFormTemplate.EntryId = UserId;
                 
                _dataContext.PdfFormTemplates.AddOrUpdate(pdfFormTemplate);
            }
             
            #endregion PDF Template..

  
            #region Field Mappings...

            foreach (GridViewRow row in gvFields.Rows)
            {
                var id = Guid.Parse(Convert.ToString(gvFields.DataKeys[row.RowIndex].Values[0]));
                var ddl = (DropDownList)row.FindControl("ddlSQLColumn");
                var selection = ddl.SelectedValue;


                if (selection.Contains("|"))
                    selection = selection.Substring(selection.IndexOf("|", StringComparison.Ordinal) + 1);

                var recored = fieldMapping.FirstOrDefault(x => x.Id.Equals(id));
                if (recored != null)
                {
                    recored.DbFieldName = selection.Trim();
                    recored.PdfFormTemplateId = pdfFormTemplateId;
                }
            }

            #endregion Field Mappings...

            #region Deep Save...
            try
            {

                foreach (var item in fieldMapping)
                {
                    var field = new FieldMapping
                    {
                        Id = Guid.NewGuid(),
                        PdfFormTemplateId = pdfFormTemplateId,
                        DbFieldName = item.DbFieldName,
                        Bottom = item.Bottom,
                        FieldType = item.FieldType,
                        Left = item.Left,
                        PdfFieldName = item.PdfFieldName,
                        PdfPageNumber = item.PdfPageNumber,
                        Right = item.Right,
                        Top = item.Top,
                        EntryDate = DateTime.Today,
                        EntryIP = CommonFunctions.GetIpAddress(),
                        IsActive = true,
                        EntryId = UserId
                    };

                    _dataContext.FieldMappings.AddOrUpdate(field);
                    _dataContext.SaveChanges();
                    lblMsg.Text = "Template saved successfully!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    lblMessage.Text += eve.Entry.Entity.GetType().Name + "<br/>" + eve.Entry.State;
                    foreach (var ve in eve.ValidationErrors)
                    {
                        lblMessage.Text += ve.PropertyName + "<br/>" + ve.ErrorMessage;
                    }
                }
            }

            #endregion Deep Save...
        }

        #region Paging and Sorting

        protected void ddlNoOfRecords_IndexChanged(object sender, EventArgs e)
        {
            ucPaging.PageNo = "1";

            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvFields, dt, ucPaging.PageNo, "First", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo_Changed(object sender, EventArgs e)
        {
            ucPaging.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];

            ucPaging.BindPaging(gvFields, dt, ucPaging.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void ImgbtnNavigator_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging.BindPaging(gvFields, dt, ucPaging.PageNo, ucPaging.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging, ucPaging1);
        }

        protected void txtPageNo1_Changed(object sender, EventArgs e)
        {
            ucPaging1.PageNo = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFields, dt, ucPaging1.PageNo, "txt", Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void ImgbtnNavigator1_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFields, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvFields_Sorting(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            SetSorting(lnkbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFields, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        protected void gvFieldsImg_Sorting(object sender, EventArgs e)
        {

            ImageButton imgbtn = (ImageButton)sender;
            SetSorting(imgbtn.CommandArgument);
            DataTable dt = Session["FilterDataTable"] == null ? (DataTable)Session["GetDataTable"] : (DataTable)Session["FilterDataTable"];
            ucPaging1.BindPaging(gvFields, dt, ucPaging1.PageNo, ucPaging1.NavType, Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
            BindBottomPaging(ucPaging1, ucPaging);
        }

        #endregion Paging and Sorting
    }
}