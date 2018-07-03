using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppTracker.Service.Models
{
    public class ApplicationLinkModel
    {
        public int Id { get; set; } // ID (Primary key)
        public string ProjectName { get; set; } // ProjectName (length: 50)
        public int TypeId { get; set; } // TypeID
        public string Description { get; set; } // Description (length: 100)
        public string Production { get; set; } // Production (length: 100)
        public string Development { get; set; } // Development (length: 100)
        public System.DateTime Created { get; set; } // Created
        public System.DateTime? Modified { get; set; } // Modified
        public System.Guid DepartmentGuid { get; set; } // DepartmentGUID

    }
}