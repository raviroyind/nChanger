using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nChanger.WebUI.Navigation
{
    public class FrmSection
    {
        public Guid FrmGuid { get; set; }
        public int DisplayOrder { get; set; }
        public string TableName { get; set; }
        public string AspxPath { get; set; }
    }
}