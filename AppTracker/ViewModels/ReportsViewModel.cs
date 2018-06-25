using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppTracker.ViewModels
{
    public class ReportsViewModel
    {
 
        public string ApplicationName { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public string ProductionLink { get; set; }
        public string DevLink { get; set; }
        public string ReportType { get; set; }
        public string ReportGrouping { get; set; }

    }
}