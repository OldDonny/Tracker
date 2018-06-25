using AppTracker.DataConnection.AppTracker;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using AppTracker.ServiceLayer.Services;

namespace AppTracker.ServiceLayer.Services
{
    public class UserRoleService
    {
        private AppRoleService allRoles = new AppRoleService();
        private MyDbContext _db;

        public void AddUserRole(string blazerId, int roleId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    UserRole newUserRole = new UserRole
                    {
                        BlazerId = blazerId,
                        RoleId = roleId
                    };
                    _db.UserRoles.Add(newUserRole);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }


        public void editUserRoll(string blazerId, int roleId)
        {
            var findUser = _db.UserRoles.Find(blazerId);

            UserRole UpdatedRole = new UserRole
            {
                BlazerId = findUser.BlazerId,
                RoleId = findUser.RoleId
            };
           
            _db.UserRoles.AddOrUpdate(UpdatedRole);
            _db.UserRoles.Remove(findUser);
            _db.SaveChanges();
        }

        

        public SelectListItem GetUserRole(string blazerId)
        {

            var UsersRole = _db.UserRoles.Find(blazerId);

            if (UsersRole==null)
            {
                var allroles = allRoles.getRoles();
                foreach (var role in allroles)
                {
                    return new SelectListItem
                    {
                        Text = role.Name,
                        Value = role.Id.ToString()
                    };
                }

            }
           return new SelectListItem()
            {
                Text = UsersRole.Role.Name,
                Value = UsersRole.Role.ToString()
            };

        }

    }
}
