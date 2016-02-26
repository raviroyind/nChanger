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
        private IQueryable<string> _columns;
        private IQueryable<string> _generalQuestions;
        private nChangerDb _dataContext = new nChangerDb();
        private bool _editMode;

        #endregion Variable Declarations...

        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            { 
                if(!string.IsNullOrEmpty(PreviousPageId))
                    hypBack.NavigateUrl= "ManagePdfTemplate.aspx?id="+PreviousPageId;
                 
                if (Request.QueryString["id"] != null)
                {
                    #region Add Quetions....

                    using (var dataContext = new nChangerDb())
                    {
                        var id = Guid.Parse(Request.QueryString["id"]);
                        var provinceCategoryId = dataContext.PdfFormTemplates.Find(id).ProvinceCategoryId;
                        _generalQuestions = from x in _dataContext.DefineQuestions.Where(x => x.ProvinceCategoryId.Equals(provinceCategoryId)) select x.ProvinceCategory.Category + " | " + x.Question;
                         
                        Session.Add("GEN_QUESTION", _generalQuestions);
                    }

                    #endregion....

                    if (Request.QueryString["active"] == "False")//Add Mode
                        LoadPdfData();
                    else if (Request.QueryString["active"] == "True")//Edit
                        Display(Guid.Parse(Request.QueryString["id"]));

                    
                }
            }
        }

        #region Functions...

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
                _editMode = true;
                
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
                else
                {
                    btnSubmit.CssClass = "ui orange button";
                    btnEditFields.Visible = false;
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
                    gvFields.Visible = true;
                }
                 
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

            if (_fieldMapping.Count == 0)
            {
                if (Session["LIST"] != null)
                {
                    _fieldMapping.AddRange((List<FieldMapping>) Session["LIST"]);
                }
            }

            var list = _fieldMapping.OrderBy(f => f.PdfPageNumber).ToList();
            var dtSearch = CommonFunctions.ToDataTable<FieldMapping>(list);

            if (dtSearch != null)
            {
                Session.Add("GetDataTable", dtSearch);
                ucPaging.BindPaging(gvFields, dtSearch, ucPaging.PageNo, "txt",
                    Convert.ToString(ViewState["sortDirection"]), Convert.ToString(ViewState["sortColumn"]));
                BindBottomPaging(ucPaging, ucPaging1);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "$('.ui.fluid.search.selection.dropdown').dropdown();", true);
            
        }
         
        private void Display(Guid id)
        {
            _editMode = false;
            var pdfFormTemplate = _dataContext.PdfFormTemplates.Find(Guid.Parse(Request.QueryString["id"]));
            if (pdfFormTemplate != null)
            {
                txtTemplateName.Text = pdfFormTemplate.TemplateName;
                hypPdf.Text = pdfFormTemplate.PdfFileName;
                hypPdf.NavigateUrl = @"../Pdf/" + pdfFormTemplate.PdfFileName;
                btnSubmit.CssClass = "ui orange button disabled";
                btnEditFields.Visible = true;
            }

            _fieldMapping.AddRange(
                            _dataContext.FieldMappings.Where(f => f.PdfFormTemplateId.Equals(pdfFormTemplate.Id))
                                .OrderBy(o => o.PdfPageNumber)
                                .ThenByDescending(o => o.Top)
                                .ToList());


            var tables = from c in _dataContext.InputFormTables
                         where c.IsActive
                         select c.TableName;

            var builder = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["nChangerDb"].ConnectionString);
            var cnnStr = builder.ProviderConnectionString;

            var sqlBuilder = new SqlConnectionStringBuilder(cnnStr);


            _columns = from c in _dataContext.InputFormSchemaViews
                       where tables.Contains(c.TABLE_NAME) && c.TABLE_SCHEMA == "dbo" && c.TABLE_CATALOG == sqlBuilder.InitialCatalog && c.COLUMN_NAME != "Id"
                      && c.COLUMN_NAME != "PdfTemplateId" && c.COLUMN_NAME != "IsActive" && c.COLUMN_NAME != "EntryDate" && c.COLUMN_NAME != "EntryIP"
                       && c.COLUMN_NAME != "EntryId" && c.COLUMN_NAME != "UserId" && c.TABLE_NAME != "InputFormSchemaView" && c.TABLE_NAME != "Users"
                       select c.TABLE_NAME + " | " + c.COLUMN_NAME;

            if (_fieldMapping.Count > 0)
            {
                BindGrid();
                Session.Add("LIST", _fieldMapping);
                gvFields.Visible = true;
            }
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
                var selectionText = ddl.SelectedItem.Text;
                var tblName = string.Empty;

                if (selection.Contains("|"))
                {
                    selection = selection.Substring(selection.IndexOf("|", StringComparison.Ordinal) + 1);
                }


                if (selectionText.Contains("|"))
                {
                    tblName = selectionText.Substring(0, selectionText.IndexOf("|", StringComparison.Ordinal) - 1);
                }


                var recored = fieldMapping.FirstOrDefault(x => x.Id.Equals(id));
                if (recored != null)
                {
                    recored.DbFieldName = selection.Trim();
                    recored.TableName = tblName.Trim();
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
                        TableName = item.TableName,
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

        private void RemoveExisting(string recordId)
        {
            var id = Guid.Parse(recordId);

            var template = _dataContext.PdfFormTemplates.Find(id);
            if (template != null)
            {
                try
                {
                    _dataContext.Database.ExecuteSqlCommand("DELETE FROM PdfFormTemplate WHERE Id='" + recordId + "'");
                    _dataContext.Database.ExecuteSqlCommand("DELETE FROM FieldMapping WHERE PdfFormTemplateId='" +
                                                            recordId + "'");
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }

            }
        }

        #endregion Functions...

        #region Events...

        protected void gvFields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblSqlColumn = (Label)e.Row.FindControl("lblSQLColumn");

                var ddlSqlColumn = (DropDownList)e.Row.FindControl("ddlSQLColumn");

                if (ddlSqlColumn != null)
                {
                    ddlSqlColumn.Visible = true;
                    lblSqlColumn.Visible = false;
                    if (Request.QueryString["active"] == "False" || _editMode)
                    {
                        var formQuestionColumns = _columns.ToArray();
                        var generalQuestionColumns = _generalQuestions.ToArray();

                        var collection = formQuestionColumns.Union(generalQuestionColumns).ToList();
                         
                        BindDropdownList(ddlSqlColumn, collection, "", "");
                       
                        var filedId = Convert.ToString(gvFields.DataKeys[e.Row.RowIndex].Values[0]);

                        var field = _dataContext.FieldMappings.Find(Guid.Parse(filedId));

                        if (field != null)
                        {
                            if (!field.DbFieldName.Equals("SEL"))
                            {
                                ddlSqlColumn.ClearSelection();
                                ddlSqlColumn.Items.FindByValue(field.TableName + " | " + field.DbFieldName)
                                                    .Selected = true;
                            }
                        }
                    }
                    else
                    {
                        ddlSqlColumn.Visible = false;
                        lblSqlColumn.Visible = !lblSqlColumn.Text.Contains("SEL");
                    }
                }
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["active"] == "True")
                RemoveExisting(Request.QueryString["id"]);
  
            try
            {
                Add();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "$('.ui.fluid.search.selection.dropdown').dropdown();", true);
                lblMsg.Text = "Template saved successfully!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showAlert()", true);
            }  
        }

        protected void btnEditFields_OnClick(object sender, EventArgs e)
        {
            _editMode = true;
            var tables = from c in _dataContext.InputFormTables
                         where c.IsActive
                         select c.TableName;

            var builder = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["nChangerDb"].ConnectionString);
            var cnnStr = builder.ProviderConnectionString;

            var sqlBuilder = new SqlConnectionStringBuilder(cnnStr);


            _columns = from c in _dataContext.InputFormSchemaViews
                       where tables.Contains(c.TABLE_NAME) && c.TABLE_SCHEMA == "dbo" && c.TABLE_CATALOG == sqlBuilder.InitialCatalog && c.COLUMN_NAME != "Id"
                      && c.COLUMN_NAME != "PdfTemplateId" && c.COLUMN_NAME != "IsActive" && c.COLUMN_NAME != "EntryDate" && c.COLUMN_NAME != "EntryIP"
                       && c.COLUMN_NAME != "EntryId" && c.COLUMN_NAME != "UserId" && c.TABLE_NAME != "InputFormSchemaView" && c.TABLE_NAME != "Users"
                       select c.TABLE_NAME + " | " + c.COLUMN_NAME;

            if (_fieldMapping.Count == 0)
                _fieldMapping.AddRange((List<FieldMapping>)Session["LIST"]);

            var id = Guid.Parse(Request.QueryString["id"]);
            var provinceCategoryId = _dataContext.PdfFormTemplates.Find(id).ProvinceCategoryId;
            _generalQuestions = from x in _dataContext.DefineQuestions.Where(x => x.ProvinceCategoryId.Equals(provinceCategoryId)) select x.ProvinceCategory.Category + " | " + x.Question;

            BindGrid();

            btnSubmit.CssClass = "ui orange button";
            btnEditFields.Visible = false;
        }

        #endregion Events...

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