using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using AjaxControlToolkit;
using nChanger.Core;

namespace nChanger.WebUI
{
    public class EMailValidateResult
    {
        public bool Valid { get; set; }
        public string Result { get; set; }
    }

    /// <summary>
    /// Summary description for services
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class services : System.Web.Services.WebService
    {

        [WebMethod]
        public EMailValidateResult ValidateEMail(string email)
        {
            var result = new EMailValidateResult {Valid = true, Result = "Valid..."};


            if(CommonFunctions.CheckIfUserExists(email))
            {  
                result.Valid = false;
                result.Result = "Email Id already exists.";
            }

            return result;
        }


        [WebMethod]
        [ScriptMethod]
        public CascadingDropDownNameValue[] GetProvinceList(string knownCategoryValues, string category)
        {
            var values = new List<CascadingDropDownNameValue>();
            using (var dataContext=new nChangerDb())
            {
                var listProvices = dataContext.Provinces.ToList();
                values.AddRange(from provice in listProvices let id = provice.Id let name = provice.ProvinceName select new CascadingDropDownNameValue(name, id.ToString()));
            }
             
            return values.ToArray();
        }
       
        [WebMethod]
        public CascadingDropDownNameValue[] GetProvinceCategoryList(string knownCategoryValues, string category)
        {
            var provId = knownCategoryValues.Substring(knownCategoryValues.IndexOf(":", StringComparison.Ordinal) + 1);
            provId = provId.Substring(0, provId.Length - 1);
            var id = Guid.Parse(provId);
            var values = new List<CascadingDropDownNameValue>();
            using (var dataContext = new nChangerDb())
            {
                var listCategories = dataContext.ProvinceCategories.Where(p => p.ProvinceId.Equals(id)).OrderBy(p => p.Category).ToList();
                values.AddRange(from provinceCategory in listCategories let pid = provinceCategory.Id.ToString() let category1 = provinceCategory.Category select new CascadingDropDownNameValue(category1, pid));
            }

            return values.ToArray();
        }
         
    }


}
