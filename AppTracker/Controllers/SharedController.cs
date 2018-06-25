using AppTracker.DataConnection.CommonObjects;
using AppTracker.ServiceLayer.DTOS;
using AppTracker.ServiceLayer.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace AppTracker.Controllers
{
    public class SharedController : Controller
    {
        EmployeeData _employeeData = new EmployeeData();
        AppService _appServiceAppList = new AppService();
        AppRoleService _appRoleService = new AppRoleService();

        //Returns list with the matching results of term from common db as Json
        public JsonResult EmployeesSearch(string term)
        {
            DataTable dt = _employeeData.EmployeeSearch(term);
            List<Select2DTO> items = dt.AsEnumerable().Select(x => new Select2DTO
            {
                id = x["BLAZERID"].ToString(),
                text = x["BLAZERID"].ToString()
            }).ToList();
            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }

        //Returns list of all Apps from Applist db as Json
        public JsonResult GetAllApps()
        {
            List<AppDTO> items = _appServiceAppList.GetAllApps().Select(x => new AppDTO
            {
                id = x.Id,
                Text = x.Name
            }).ToList();
            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }

        //Returns list of all AppRoles from AppTracker db as Json
        public JsonResult GetAllAppRoles()
        {
            List<AppRoleDTO> items = _appRoleService.getRoles().Select(x => new AppRoleDTO
            {
                id = x.Id,
                Text = x.Name
            }).ToList();
            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }

        //Returns list of AppModels by blazerId --- Important!! the id paramater of this method is blazerId
        public JsonResult GrabUserApps(string id)
        {
            List<AppDTO> items = _appServiceAppList.Grabusersapps(id).Select(x => new AppDTO
            {
                id = x.Id,
                Text = x.Name
            }).ToList();
            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }

        //Important!! the id paramater of this method is blazerId
        public JsonResult GrabUserAppRoles(string id)
        {
            List<AppRoleDTO> items = _appRoleService.GrabUserAppRoles(id).Select(x => new AppRoleDTO
            {
                id = x.Id,
                Text = x.Name
            }).ToList();
            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }
    }
}