using AppTracker.DataConnection.AppTracker;
using AppTracker.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AppTracker.ServiceLayer.Services
{
    public class UserAppService
    {
        //Working
        public List<UserAppModel> GetAllUserAppsDistinct()
        {
            List<UserAppModel> list = new List<UserAppModel>();

            using (MyDbContext _db = new MyDbContext())
            {
                try
                {

                    var distinctBlazerIds = _db.UserApps.GroupBy(x => x.BlazerId).Select(x => x.FirstOrDefault()).ToList();

                    foreach (var item in distinctBlazerIds)
                    {
                        var userAppModel = new UserAppModel
                        {
                            BlazerId = item.BlazerId,
                            AppCount = _db.UserApps.Count(x => x.BlazerId == item.BlazerId)
                        };
                        list.Add(userAppModel);
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

        //Working
        public void AddUserApps(string blazerId, List<int> appIds, List<int> roleIds)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    var counter = 0;

                    while (counter <= appIds.Count - 1)
                    {
                        var newUserApp = new UserApp
                        {
                            BlazerId = blazerId,
                            AppId = appIds[counter],
                            RoleId = roleIds[counter]
                        };
                        _db.UserApps.Add(newUserApp);
                        _db.SaveChanges();
                        counter++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        //Working
        public void DeleteUserApps(string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    var instancesOfUser = _db.UserApps.Where(x => x.BlazerId == blazerId).ToList();
                    _db.UserApps.RemoveRange(instancesOfUser);

                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public List<int> GetAppIdsWhereAppRoleAdmin(string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    var appIds = _db.UserApps.Where(x => x.BlazerId == blazerId && x.RoleId == 1).Select(x => x.AppId).ToList();
                    return appIds;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public List<UserAppModel> GetUserAppsForAppIds(List<int> appIds)
        {
            //get UserApp foreach item in appIds
            using (MyDbContext _db = new MyDbContext())
            {
                List<UserAppModel> list = new List<UserAppModel>();
                try
                {
                    foreach (var Id in appIds)
                    {
                        var userApps = _db.UserApps.Where(x => x.AppId == Id).Select(x => new UserAppModel
                        {
                            BlazerId = x.BlazerId,
                            AppId = x.AppId,
                            AppRoleId = x.RoleId,
                        }).ToList();

                        list.AddRange(userApps);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return list.GroupBy(x => x.BlazerId).Select(x => x.FirstOrDefault()).ToList();
            }
            //Store item in a list
            //return list
        }

        public int CountUserApps(string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    return _db.UserApps.Count(x => x.BlazerId == blazerId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }


        public void appsToRemove(string BlazerId, List<Guid> appGuids)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    var selected = _db.UserApps.Where(x => x.BlazerId == BlazerId);

                    var rejects = selected.Where(x => appGuids.Contains(x.Guid)).ToList();

                    var appsToRemove = selected.Except(rejects).ToList();

                    _db.UserApps.RemoveRange(appsToRemove);
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
