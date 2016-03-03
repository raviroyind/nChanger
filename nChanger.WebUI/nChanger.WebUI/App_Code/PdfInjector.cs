using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using nChanger.Core;

namespace nameChanger.WebUI
{
    public static class PdfInjector
    {
        public static string FillForm(Guid pdfTemplateId, string userId)
        {
            var dataContext = new nChangerDb();

            var pdfTemplate = dataContext.PdfFormTemplates.Find(pdfTemplateId);

            if (pdfTemplate == null)
                return string.Empty;

            var pdfToFill = HttpContext.Current.Server.MapPath("../Pdf/" + pdfTemplate.PdfFileName);
            var newFile =
                HttpContext.Current.Server.MapPath("../Output/" + pdfTemplate.TemplateName + "_" + userId + ".pdf");
            var pdfReader = new PdfReader(pdfToFill);
            PdfReader.unethicalreading = true;
            var pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create)) { FormFlattening = true };
            var pdfFormFields = pdfStamper.AcroFields;


            #region General Questions...

            var generalQuestions = dataContext.GeneralQuestionUserResponses.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfFormTemplateId.Equals(pdfTemplateId));

            if (generalQuestions != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in generalQuestions.PdfFormTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = (from g in dataContext.GeneralQuestionUserResponses
                                                       where g.Question.Equals(mapping.DbFieldName) && g.PdfFormTemplateId.Equals(pdfTemplateId) && g.UserId.Equals(userId)
                                                       select g.UserAnswer).SingleOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? GetPdfYesNo(result) : result);
                            }
                            catch (Exception exception)
                            {
                            }
                        }
                    }
                }
            }

            #endregion General Questions...

            #region OnPersonalInformations...

            var frmOn = dataContext.PersonalInformations.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfFormTemplateId.Equals(pdfTemplateId));

            if (frmOn != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmOn.PdfFormTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.PersonalInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? GetPdfYesNo(result) : result);
                            }
                            catch (Exception exception)
                            {
                            }
                        }
                    }
                }
            }

            #endregion OnPersonalInformations...

            #region ParentInformation...

            var frmParentInformation = dataContext.ParentInformations.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfFormTemplateId.Equals(pdfTemplateId));

            if (frmParentInformation != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmParentInformation.PdfFormTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.ParentInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? GetPdfYesNo(result) : result);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }

            #endregion ParentInformation...

            #region NameChangeInformation...

            var frmNameChangeInformation = dataContext.NameChangeInformations.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfFormTemplateId.Equals(pdfTemplateId));

            if (frmNameChangeInformation != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmNameChangeInformation.PdfFormTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.NameChangeInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? GetPdfYesNo(result) : result);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }

            #endregion NameChangeInformation...   

            #region CriminalOffenceInformation...

            var frmCriminalOffenceInformation = dataContext.CriminalOffenceInformations.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfFormTemplateId.Equals(pdfTemplateId));

            if (frmCriminalOffenceInformation != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    if (kvp.Key.Equals("court_number"))
                    {
                        var fieldnm = kvp.Key;
                    }


                    foreach (var mapping in frmCriminalOffenceInformation.PdfFormTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.CriminalOffenceInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? GetPdfYesNo(result) : result);
                            }
                            catch (Exception exception)
                            {
                                 
                            }
                        }
                    }
                }
            }

            #endregion CriminalOffenceInformation...   

            #region FinancialInformation...

            var frmFinancialInformation = dataContext.FinancialInformations.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfFormTemplateId.Equals(pdfTemplateId));

            if (frmFinancialInformation != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmFinancialInformation.PdfFormTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.FinancialInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? GetPdfYesNo(result) : result);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }

            #endregion FinancialInformation... 

            pdfStamper.Close();

           return  newFile;
             
        }

        public static string GetPdfYesNo(string result)
        {
            var returnValue = String.Empty;

            switch (result.Trim().ToLower())
            {
                case "y":
                    returnValue = "yes";
                    break;
                case "n":
                    returnValue = "no";
                    break;
                case "1":
                    returnValue = "yes";
                    break;
                case "0":
                    returnValue = "no";
                    break;
                case "true":
                    returnValue = "yes";
                    break;
                case "false":
                    returnValue = "no";
                    break;
            }

            return returnValue;
        }
    }
}