using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace nChanger.WebUI
{
    
    public class AppConfig
    { 

        #region Security Settings

        public static string SessionItemNameUserId = "USER_KEY";
        public static string SessionItemNameUserName = "USR_NAME";
        public static string SessionItemNameUserType = "USER_TYPE";
        public static string PreviousPageId = "PREV_KEY_PROVCAT";
        public static string CurrentId = "CURRENT_ID";
        public static string RecordId = "RECORD_ID";
        public static string CurrentFormIndex = "CURRENT_FRM_INDEX";
        public static string FormSetions = "FRM_SECTIONS";
        public static string Paid = "IS_PAID";
        #endregion

    }
}
