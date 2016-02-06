using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

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
    }


}
