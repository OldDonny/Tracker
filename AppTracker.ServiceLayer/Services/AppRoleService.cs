using AppTracker.DataConnection.AppTracker;
using AppTracker.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTracker.ServiceLayer.Services
{
    public class AppRoleService
    {
        private MyDbContext _db = new MyDbContext();

        public List<AppRoleModel> getRoles()
        {
            try
            {
                return _db.AppRoles.Select(x => new AppRoleModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<int> GrabUserAppRoleIds(string blazerId)
        {
            List<int> usersAppRoleIds = _db.UserApps.Where(x => x.BlazerId == blazerId).Select(x => x.RoleId).ToList();

            return usersAppRoleIds;
        }

        public List<AppRoleModel> GrabUserAppRoles(string blazerId)
        {
            List<AppRoleModel> list = new List<AppRoleModel>();
            try
            {
                List<int> roleIds = GrabUserAppRoleIds(blazerId);
                foreach (var Id in roleIds)
                {
                    var appRole = _db.AppRoles.Single(x => x.Id == Id);
                    var appRoleModel = new AppRoleModel()
                    {
                        Id = appRole.Id,
                        Name = appRole.Name
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
}
