using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppTracker.ViewModels
{
    public class EditUserViewModel
    {
        public string BlazerId { get; set; }
        public SelectList Roles { get; set; }
        public IEnumerable<SelectListItem> Apps { get; set; }
    }
}