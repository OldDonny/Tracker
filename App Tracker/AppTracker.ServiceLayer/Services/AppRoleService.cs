using AppTracker.DataConnection.AppTracker;
using AppTracker.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTracker.ServiceLayer.Services
{
    public class AppRoleService
    {
        //Working
        public List<AppRoleModel> getRoles()
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    return _db.AppRoles.Select(x => new AppRoleModel
                    {
                        RoleId = x.Id,
                        RoleName = x.Name
                    }).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        //The following 2 methods grab the existing UserApp roles for the Edit dropdown
        //Working
        public List<int> GrabUserAppRoleIds(string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    List<int> usersAppRoleIds = _db.UserApps.Where(x => x.BlazerId == blazerId).Select(x => x.RoleId).ToList();
                    return usersAppRoleIds;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        //Working
        public List<AppRoleModel> GrabUserAppRoles(string blazerId)
        {
            List<AppRoleModel> list = new List<AppRoleModel>();

            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    List<int> roleIds = GrabUserAppRoleIds(blazerId);
                    foreach (var Id in roleIds)
                    {
                        var appRole = _db.AppRoles.Single(x => x.Id == Id);
                        var appRoleModel = new AppRoleModel()
                        {
                            RoleId = appRole.Id,
                            RoleName = appRole.Name
                        };
                        list.Add(appRoleModel);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return list;
            }

        }
        //Carefull this is for edit to get the existing rows
        public AppRoleModel BGrabAppRoles(int appId, string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                var appRoleModel = new AppRoleModel();
                try
                {
                    var userApp = _db.UserApps.FirstOrDefault(x => x.BlazerId == blazerId && x.AppId == appId);

                    if (userApp != null)
                    {
                        appRoleModel.RoleId = userApp.RoleId;
                        appRoleModel.RoleName = GetAppRoleNameById(userApp.RoleId);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return appRoleModel;
            }
        }

        public string GetAppRoleNameById(int roleId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    var appRole = _db.AppRoles.Single(x => x.Id == roleId);
                    return appRole.Name;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
