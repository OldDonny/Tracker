using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AppTracker.Service.Models
{
    public class AspNetUserModel
    {
        public class AspNetUser
        {
            public string Id { get; set; } // Id (Primary key) (length: 128)
            public string AppId { get; set; } // AppId (length: 128)
            public string Email { get; set; } // Email (length: 256)
            public bool EmailConfirmed { get; set; } // EmailConfirmed
            public string PasswordHash { get; set; } // PasswordHash
            public string SecurityStamp { get; set; } // SecurityStamp
            public string PhoneNumber { get; set; } // PhoneNumber
            public bool PhoneNumberConfirmed { get; set; } // PhoneNumberConfirmed
            public bool TwoFactorEnabled { get; set; } // TwoFactorEnabled
            public System.DateTime? LockoutEndDateUtc { get; set; } // LockoutEndDateUtc
            public bool LockoutEnabled { get; set; } // LockoutEnabled
            public int AccessFailedCount { get; set; } // AccessFailedCount
            public string UserName { get; set; } // UserName (length: 256)
            public string FullName { get; set; } // FullName (length: 255)

           
        }
    }
}