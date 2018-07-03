using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppTracker.Service.Models
{
    public class AspNetRoleModel
    {
        public string Id { get; set; } // Id (Primary key) (length: 128)
        public string Name { get; set; } // Name (length: 256)
    }
}