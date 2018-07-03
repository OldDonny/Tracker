using System.Collections.Generic;

namespace AppTracker.ServiceLayer.Models
{
    public class UserModel
    {
        public string BlazerId { get; set; }
        public List<int> RoleIds { get; set; }
        public List<int> AppIds { get; set; }
    }
}
