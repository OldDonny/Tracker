using AppTracker.DataConnection.CommonObjects;
using AppTracker.ServiceLayer.DTOS;
using AppTracker.ServiceLayer.Models;
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
        UserRoleService _userRoleService = new UserRoleService();

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
            //Logic working before Super Admins
            //List<AppDTO> items = _appServiceAppList.GetAllApps().Select(x => new AppDTO
            //{
            //    id = x.Id,
            //    Text = x.Name
            //}).ToList();
            //return Json(new { items }, JsonRequestBehavior.AllowGet);

            List<AppDTO> items = new List<AppDTO>();

            var isAdmin = _userRoleService.IsSuperAdmin(User.Identity.Name);

            if (isAdmin)
            {
                var appList = _appServiceAppList.GetAllApps();

                foreach (var app in appList)
                {
                    var appDto = new AppDTO
                    {
                        id = app.Id,
                        Text = app.Name
                    };
                    items.Add(appDto);
                }
            }
            else
            {
                var appList = _appServiceAppList.GrabAdminUserApps(User.Identity.Name);

                foreach (var app in appList)
                {
                    var appDto = new AppDTO
                    {
                        id = app.Id,
                        Text = app.Name
                    };
                    items.Add(appDto);
                }
            }

            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }

        //Returns list of all AppRoles from AppTracker db as Json
        public JsonResult GetAllAppRoles()
        {
            List<AppRoleDTO> items = _appRoleService.getRoles().Select(x => new AppRoleDTO
            {
                id = x.RoleId,
                Text = x.RoleName
            }).ToList();
            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }

        //Returns list of AppDTOS by blazerId --- Important!! the id paramater of this method is blazerId
        public JsonResult GrabUserApps(string id)
        {
            //List<AppDTO> items = _appServiceAppList.Grabusersapps(id).Select(x => new AppDTO
            //{
            //    id = x.Id,
            //    Text = x.Name
            //}).ToList();
            //return Json(new { items }, JsonRequestBehavior.AllowGet);

            List<AppDTO> items = new List<AppDTO>();

            var isAdmin = _userRoleService.IsSuperAdmin(User.Identity.Name);

            if (isAdmin)
            {
                var appList = _appServiceAppList.Grabusersapps(id);

                foreach (var app in appList)
                {
                    var appDto = new AppDTO
                    {
                        id = app.Id,
                        Text = app.Name
                    };
                    items.Add(appDto);
                }
            }
            else
            {
                var appList = _appServiceAppList.GrabAdminUserApps(User.Identity.Name);

                foreach (var app in appList)
                {
                    var appDto = new AppDTO
                    {
                        id = app.Id,
                        Text = app.Name
                    };
                    items.Add(appDto);
                }
            }

            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }

        //Returns list of AppDTOS by blazerId --- Important!! the id paramater of this method is blazerId
        public JsonResult GrabUserAppRoles(string id)
        {
            List<AppRoleDTO> items = _appRoleService.GrabUserAppRoles(id).Select(x => new AppRoleDTO
            {
                id = x.RoleId,
                //AppRoleId = x.AppId,
                Text = x.RoleName
            }).ToList();
            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }

        //Returns list of UserAppRowDTOS by blazerId --- Important!! the id paramater of this method is blazerId
        public JsonResult GrabUserAppsAndRoles(string id)
        {
            List<UserAppRowDTO> items = new List<UserAppRowDTO>();

            var isAdmin = _userRoleService.IsSuperAdmin(User.Identity.Name);

            if (isAdmin)
            {
                var appList = _appServiceAppList.Grabusersapps(id);
                //grab the roleid foreach app in appList
                var roleList = new List<AppRoleModel>();
                foreach (var app in appList)
                {
                    roleList.Add(_appRoleService.BGrabAppRoles(app.Id, id));
                }

                var counter = 0;

                while (counter < appList.Count)
                {
                    var userAppRowDto = new UserAppRowDTO
                    {
                        Id = counter,
                        AppId = appList[counter].Id,
                        AppName = appList[counter].Name,
                        RoleId = roleList[counter].RoleId,
                        RoleName = roleList[counter].RoleName
                    };
                    items.Add(userAppRowDto);
                    counter++;
                }
            }
            else
            {
                var appIdsWhereAdmin = _appServiceAppList.GrabAdminUserAppIds(User.Identity.Name);

                var counter = 0;
                foreach (var appId in appIdsWhereAdmin)
                {
                    var userHasApp = _appServiceAppList.CheckIfUserHasApp(appId, id);
                    if (userHasApp)
                    {
                        //Get the app Name, RoleID and RoleName for that appId
                        var appRoleModel = _appRoleService.BGrabAppRoles(appId, id);
                        var userAppRowDto = new UserAppRowDTO
                        {
                            Id = counter,
                            AppId = appId,
                            AppName = _appServiceAppList.GrabAppName(appId),
                            RoleId = appRoleModel.RoleId,
                            RoleName = appRoleModel.RoleName
                        };
                        items.Add(userAppRowDto);
                    }
                }
                ////Grab userApps for the loggedin person where AppRoleId is admin
                //var appList = _appServiceAppList.GrabAdminUserApps(User.Identity.Name);

                //var roleList = new List<AppRoleModel>();

                ////if 
                //foreach (var app in appList)
                //{
                //    roleList.Add(_appRoleService.BGrabAppRoles(app.Id, id));
                //}

                ////

                //var counter = 0;

                //while (counter < appList.Count)
                //{
                //    var userAppRowDto = new UserAppRowDTO
                //    {
                //        Id = counter,
                //        AppId = appList[counter].Id,
                //        AppName = appList[counter].Name,
                //        RoleId = roleList[counter].RoleId,
                //        RoleName = roleList[counter].RoleName
                //    };
                //    items.Add(userAppRowDto);
                //    counter++;
                //}
            }
            //items = items.OrderBy(x => x.AppId).ToList();
            return Json(new { items }, JsonRequestBehavior.AllowGet);
        }
    }
}