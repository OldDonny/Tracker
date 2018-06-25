using AppTracker.ServiceLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppTracker.ViewModels
{
    public class UserFormViewModel
    {
        [Required(ErrorMessage = "this field is required")]
        public string BlazerId { get; set; }
        public List<AppModel> Apps { get; set; }
        public List<AppRoleModel> AppRoles { get; set; }

        public string PageTitle
        {
            get
            {
                if (BlazerId != null)
                {
                    return "Edit";
                }

                return "Add User";
            }
        }
    }
}