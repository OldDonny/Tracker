using AppTracker.DataConnection.AppTracker;
using AppTracker.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace AppTracker.ServiceLayer.Services
{
    public class UserRoleService
    {
        

        //Working
        public List<RoleModel> GetRoles()
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    return _db.Roles.Select(x => new RoleModel
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
        }

        public int GetUserRoleId(string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    var userRole = _db.UserRoles.Single(x => x.BlazerId == blazerId);
                    return userRole.RoleId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

        }

        //Working
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

        public void DeleteUserRole(string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    var instancesOfUser = _db.UserRoles.Where(x => x.BlazerId == blazerId).ToList();
                    _db.UserRoles.RemoveRange(instancesOfUser);

                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public bool IsSuperAdmin(string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    var userRole = _db.UserRoles.Single(x => x.BlazerId == blazerId);

                    return userRole.RoleId == 1 ? true : false;
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

            using (MyDbContext _db = new MyDbContext())
            {
                try
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
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            



        }

        //Reeds GetUserRole commented out for now I dont need a selectlist
        //public SelectListItem GetUserRole(string blazerId)
        //{

        //    var UsersRole = _db.UserRoles.Find(blazerId);

        //    if (UsersRole == null)
        //    {
        //        var allroles = allRoles.getRoles();
        //        foreach (var role in allroles)
        //        {
        //            return new SelectListItem
        //            {
        //                Text = role.Name,
        //                Value = role.Id.ToString()
        //            };
        //        }

        //    }
        //    return new SelectListItem()
        //    {
        //        Text = UsersRole.Role.Name,
        //        Value = UsersRole.Role.ToString()
        //    };

        //}

    }
}
