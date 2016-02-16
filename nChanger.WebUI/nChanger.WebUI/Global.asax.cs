using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace nChanger.WebUI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
             new ScriptResourceDefinition
             {
                 Path = "~/scripts/jquery-1.8.1.min.js",
                 DebugPath = "~/scripts/jquery-1.8.1.min.js",
                 CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.min.js",
                 CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.js"
             });
        }


        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Session["UnAuthorizedAccess"] = new byte();
        }
    }
}