﻿using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using nameChanger.WebUI;
using nChanger.Core;


namespace nChanger.WebUI.Forms
{
    public partial class FrmParentInformation : AppBasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormIndex = 2;
                Display();
            }
        }

        private void Display()
        {
            try
            {
                var id = Guid.Parse(RecordId);

                hypBack.NavigateUrl = "../Forms/frmONPersonalInfo.aspx?id=" + RecordId;

                using (var dataContext = new nChangerDb())
                {
                    var parentInformation = dataContext.ParentInformations.Find(id);

                    if (parentInformation != null)
                    {
                        
                        txtFatherFirstName.Text = parentInformation.FatherFirstName;	
                        txtFatherMiddleName.Text = parentInformation.FatherMiddleName;
                        txtFatherLastName.Text = parentInformation.FatherLastName;
                        txtFatherLastNameOther.Text = parentInformation.FatherOtherLastName;
                        txtMotherFirstName.Text = parentInformation.MotherFirstName;
                        txtMotherMiddleName.Text = parentInformation.MotherMiddleName;
                        txtMotherLastNameBorn.Text = parentInformation.MotherLastNameWhenBorn;
                        txtMotherLastNamePresent.Text = parentInformation.MotherLastNamePresent;
                        txtMotherLastNameOther.Text = parentInformation.MotherLastNameOther;
                    }
                     
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

            try
            {
                divMsg.InnerText = Submit();
                var sections = Sections;
                var page = Path.GetFileName(Request.PhysicalPath);
                var curret = Sections.FirstOrDefault(s => s.AspxPath.Contains(page));

                using (var dataContext = new nChangerDb())
                {
                    dataContext.Database.ExecuteSqlCommand("DELETE FROM UserFormDetail WHERE UserId='" + UserId +
                                                           "' AND PdfTemplateId='" + CurrentId + "'  AND FrmGuid='" +
                                                           curret.FrmGuid.ToString() + "'");

                    dataContext.UserFormDetails.AddOrUpdate(new UserFormDetail
                    {
                        Id = Guid.NewGuid(),
                        UserId = UserId,
                        AspxPath = curret.AspxPath,
                        TableName = curret.TableName,
                        PdfTemplateId = Guid.Parse(CurrentId),
                        FrmGuid = curret.FrmGuid,
                        Completed = "Y"
                    });

                    dataContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            var nextPage = FormIndex + 1;
            var redirect = Sections.Where(s => s.DisplayOrder.Equals(nextPage)).FirstOrDefault().AspxPath;

            Response.Redirect(redirect);
        }

        private string Submit()
        {

            var id = Guid.Parse(RecordId);
            var returnMessage = string.Empty;
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var dbEntry = dataContext.ParentInformations.Find(id);
                     
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
                            Id = id,
                            PdfFormTemplateId = Guid.Parse(CurrentId),
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
                    
                }
            }
            catch (DbEntityValidationException ex)
            {
                returnMessage = ex.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors).Aggregate(returnMessage, (current, ve) => current + (ve.PropertyName + " Error Msg:" + ve.ErrorMessage));
            }

            return returnMessage;
        }
    }
}