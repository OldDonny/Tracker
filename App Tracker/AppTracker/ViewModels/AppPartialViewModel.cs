using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using AppTracker.ServiceLayer.Models;

namespace AppTracker.ViewModels
{
    public class AppPartialViewModel
    {

        //List<AppModel> app { get; set; }
        public int Id { get; set; } // ID (Primary key)
        public string Name { get; set; } // Name (length: 300)
        public string Description { get; set; } // Description (length: 500)
        public string ProductionUrl { get; set; } // ProductionURL (length: 300)
        public string TestUrl  { get; set; } // TestURL (length: 300)
        public System.DateTime? EndDateTime { get; set; } // EndDateTime
    

    }
}