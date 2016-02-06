﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using nChanger.Core;
 

namespace nChanger.WebUI
{
    public static class PdfInjector
    {
        public static string FillForm(Guid pdfTemplateId, string userId)
        {
            var dataContext = new nChangerCore();

            var pdfTemplate = dataContext.PdfTemplates.Find(pdfTemplateId);

            if (pdfTemplate == null)
                return string.Empty;

            var pdfToFill = HttpContext.Current.Server.MapPath("../Pdf/" + pdfTemplate.TemplateName + ".pdf");
            var newFile =
                HttpContext.Current.Server.MapPath("../Output/" + pdfTemplate.TemplateName + "_" + userId + ".pdf");
            var pdfReader = new PdfReader(pdfToFill);
            PdfReader.unethicalreading = true;
            var pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create)) { FormFlattening = true };
            var pdfFormFields = pdfStamper.AcroFields;


            #region OnPersonalInformations...

            var frmOn = dataContext.OnPersonalInformations.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfTemplateId.Equals(pdfTemplateId));

            if (frmOn != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmOn.PdfTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.OnPersonalInformations", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? "yes" : result);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }

            #endregion OnPersonalInformations...

            
            #region ParentInformation...

            var frmParentInformation = dataContext.ParentInformations.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfTemplateId.Equals(pdfTemplateId));

            if (frmParentInformation != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmParentInformation.PdfTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {

                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.ParentInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? "yes" : result);
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
                                f => f.UserId.Equals(userId) && f.PdfTemplateId.Equals(pdfTemplateId));

            if (frmNameChangeInformation != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmNameChangeInformation.PdfTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.NameChangeInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? "yes" : result);
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
                                f => f.UserId.Equals(userId) && f.PdfTemplateId.Equals(pdfTemplateId));

            if (frmCriminalOffenceInformation != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmCriminalOffenceInformation.PdfTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.CriminalOffenceInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? "yes" : result);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }

            #endregion CriminalOffenceInformation...   

            #region FinancialInformation...

            var frmFinancialInformation = dataContext.FinancialInformations.FirstOrDefault(
                                f => f.UserId.Equals(userId) && f.PdfTemplateId.Equals(pdfTemplateId));

            if (frmFinancialInformation != null)
            {
                foreach (var kvp in pdfFormFields.Fields)
                {
                    var fieldType = pdfFormFields.GetFieldType(kvp.Key);
                    if (fieldType == 1)
                        continue;

                    foreach (var mapping in frmFinancialInformation.PdfTemplate.FieldMappings)
                    {

                        if (mapping.PdfFieldName.Equals(kvp.Key))
                        {
                            try
                            {
                                var result = dataContext.uspSelectValueByColumnName(mapping.DbFieldName,
                                    "dbo.FinancialInformation", userId).FirstOrDefault();

                                pdfFormFields.SetField(mapping.PdfFieldName, fieldType == 2 ? "yes" : result);
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