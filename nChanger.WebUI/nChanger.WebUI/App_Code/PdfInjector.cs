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
        public static string FillForm(Guid pdfFormTemplateId, Guid recordId, string userId)
        {
            var dataContext = new nChangerDb();

            var pdfTemplate = dataContext.PdfFormTemplates.Find(pdfFormTemplateId);

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

            var generalQuestions = dataContext.GeneralQuestionUserResponses.Where(g => g.RecordId.Equals(recordId)).ToList();

            if (generalQuestions.Count > 0)
            {

                foreach (var generalQuestion in generalQuestions)
                {
                    foreach (var kvp in pdfFormFields.Fields)
                    {
                        var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                        if (fieldType == 1)
                            continue;

                        foreach (var mapping in generalQuestion.PdfFormTemplate.FieldMappings)
                        {

                            if (mapping.PdfFieldName.Equals(kvp.Key))
                            {
                                try
                                {
                                    var result = (from g in dataContext.GeneralQuestionUserResponses
                                                  where g.Question.Equals(mapping.DbFieldName) && 
                                                  g.PdfFormTemplateId.Equals(pdfFormTemplateId) && 
                                                  g.UserId.Equals(userId) &&
                                                  g.RecordId.Equals(recordId) 
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
                
            }

            #endregion General Questions...

            #region OnPersonalInformations...

            var frmOn = dataContext.PersonalInformations.Find(recordId);

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

            var frmParentInformation = dataContext.ParentInformations.Find(recordId);

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

            var frmNameChangeInformation = dataContext.NameChangeInformations.Find(recordId);

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

            var frmCriminalOffenceInformation = dataContext.CriminalOffenceInformations.Find(recordId);

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

            var frmFinancialInformation = dataContext.FinancialInformations.Find(recordId);

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