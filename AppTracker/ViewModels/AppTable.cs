using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppTracker.ViewModels
{
    public class AppTable
    {
        
            //List<AppModel> app { get; set; }
            public int Id { get; set; } // ID (Primary key)
            public string Name { get; set; } // Name (length: 300)
            public string Description { get; set; } // Description (length: 500)
            public string ProductionUrl { get; set; } // ProductionURL (length: 300)
            public string TestUrl { get; set; } // TestURL (length: 300)
            public string Database { get; set; } // Database (length: 300)
            public string TestDatabase { get; set; } // TestDatabase (length: 300)
            public System.DateTime? EndDateTime { get; set; } // EndDateTime
            public string DevIp { get; set; } // DevIP (length: 500)
            public string ProdIp { get; set; } // ProdIP (length: 500)
        
    }
}