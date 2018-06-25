using AppTracker.DataConnection.AppList;
using AppTracker.DataConnection.AppTracker;
using AppTracker.ServiceLayer.Services;
using AppTracker.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace AppTracker.Controllers
{
    public class UserController : Controller
    {
        private AppRoleService _appRoleService = new AppRoleService();
        UserAppService _userAppService = new UserAppService();
        AppService _appService = new AppService();
        AppPartialViewModel AppVM = new AppPartialViewModel();
        private AppListDBContext _db;
        private MyDbContext _localDB;

        public ActionResult Index()
        {
            var vM = new UserIndexViewModel
            {
                UserApps = _userAppService.GetAllUserAppsDistinct()
            };
            return View(vM);
        }

        public ActionResult New()
        {
            var vM = new UserFormViewModel();
            return View("UserForm", vM);
        }

        public ActionResult Edit(string id)
        {
            var vM = new UserFormViewModel
            {
                BlazerId = id
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

            var roleIds = user.AppRoles.Select(x => x.Id).ToList();
            var appIds = user.Apps.Select(x => x.Id).ToList();

            _userAppService.AddUserApps(user.BlazerId, appIds, roleIds);

            return RedirectToAction("Index", "User");
        }

    }
}

