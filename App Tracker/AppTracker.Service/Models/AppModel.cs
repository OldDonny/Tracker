using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppTracker.Service.Models
{
    public class AppModel
    {
        public System.Guid Id { get; set; } // Id (Primary key)
        public string UserId { get; set; } // UserId (length: 128)
        public string AppId { get; set; } // AppId (length: 128)

      
    }
}