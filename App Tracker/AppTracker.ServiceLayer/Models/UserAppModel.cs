using System.Collections.Generic;

namespace AppTracker.ServiceLayer.Models
{
    public class UserAppModel
    {
        public string BlazerId { get; set; }
        public List<int> RoleIds { get; set; }
        public List<int> AppIds { get; set; }
        public int AppCount { get; set; }
        public int AppId { get; set; }
        public int AppRoleId { get; set; }
    }
}
