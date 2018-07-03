using AppTracker.ServiceLayer.Models;
using AppTracker.ServiceLayer.Services;
using AppTracker.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AppTracker.Controllers
{
    public class UserController : Controller
    {

        UserAppService _userAppService = new UserAppService();
        UserRoleService _userRoleService = new UserRoleService();
        public ActionResult Index()
        {
            UserIndexViewModel vM = new UserIndexViewModel();
            var isAdmin = _userRoleService.IsSuperAdmin(User.Identity.Name);

            if (isAdmin)
            {
                vM.UserApps = _userAppService.GetAllUserAppsDistinct();
            }
            else
            {
                var appIds = _userAppService.GetAppIdsWhereAppRoleAdmin(User.Identity.Name);
                var userAppModelsWithOutAppCount = _userAppService.GetUserAppsForAppIds(appIds);

                var userAppModels = new List<UserAppModel>();
                foreach (var item in userAppModelsWithOutAppCount)
                {
                    item.AppCount = _userAppService.CountUserApps(item.BlazerId);
                    userAppModels.Add(item);
                }
                vM.UserApps = userAppModels;
            }

            return View(vM);
        }

        public ActionResult New()
        {
            var vM = new UserFormViewModel
            {
                Roles = _userRoleService.GetRoles()

            };
            return View("UserForm", vM);
        }

        //Important parameter id is BlazerId
        public ActionResult Edit(string id)
        {
            var vM = new UserFormViewModel
            {
                BlazerId = id,
                RoleId = _userRoleService.GetUserRoleId(id),
                Roles = _userRoleService.GetRoles()
            };
            return View("UserForm", vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UserFormViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("New", "User");
            }
            _userAppService.DeleteUserApps(user.BlazerId);
            _userRoleService.DeleteUserRole(user.BlazerId);

            var roleIds = user.AppRoles.Select(x => x.Id).ToList();
            var appIds = user.Apps.Select(x => x.Id).ToList();

            _userAppService.AddUserApps(user.BlazerId, appIds, roleIds);
            _userRoleService.AddUserRole(user.BlazerId, user.RoleId);

            return RedirectToAction("Index", "User");
        }

    }
}

