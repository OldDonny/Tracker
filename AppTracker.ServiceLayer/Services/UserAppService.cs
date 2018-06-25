using AppTracker.DataConnection.AppTracker;
using AppTracker.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AppTracker.ServiceLayer.Services
{
    public class UserAppService
    {
        AppService appService = new AppService();


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
                    var counter = appIds.Count - 1;

                    while (counter >= 0)
                    {
                        var newUserApp = new UserApp
                        {
                            BlazerId = blazerId,
                            AppId = appIds[counter],
                            RoleId = roleIds[counter]
                        };
                        _db.UserApps.Add(newUserApp);
                        counter--;
                    }
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

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


    }
}
