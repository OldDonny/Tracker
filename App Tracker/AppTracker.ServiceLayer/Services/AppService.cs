using AppTracker.DataConnection.AppList;
using AppTracker.DataConnection.AppTracker;
using AppTracker.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTracker.ServiceLayer.Services
{
    public class AppService
    {
        AppListDBContext _db = new AppListDBContext();
        MyDbContext _localDB = new MyDbContext();

        public List<AppModel> GetAllApps()
        {
            try
            {
                _db = new AppListDBContext();
                return _db.Applications.Select(x => new AppModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ProductionUrl = x.ProductionUrl,
                    FriendlyUrl = x.FriendlyUrl,
                    TestUrl = x.TestUrl,
                    Database = x.Database,
                    DatabaseVersion = x.DatabaseVersion,
                    DatabaseInstance = x.DatabaseInstance,
                    TestDatabase = x.TestDatabase,
                    WebServer = x.WebServer,
                    MvcVersion = x.MvcVersion,
                    NetVersion = x.NetVersion,
                    AppPoolName = x.AppPoolName,
                    FilePath = x.FilePath,
                    TfsPath = x.TfsPath,
                    EndDateTime = x.EndDateTime,
                    DevIp = x.DevIp,
                    ProdIp = x.ProdIp
                })
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<int> GrabUsersAppIds(string blazerId)
        {
            List<int> usersAppIds = _localDB.UserApps.Where(x => x.BlazerId == blazerId).Select(x => x.AppId).ToList();

            return usersAppIds;
        }

        public List<AppModel> Grabusersapps(string blazerId)
        {
            List<int> userInstances = GrabUsersAppIds(blazerId);
            List<AppModel> apps = _db.Applications.Where(x => userInstances.Contains(x.Id)).Select(x =>

                new AppModel
                {

                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ProductionUrl = x.ProductionUrl,
                    FriendlyUrl = x.FriendlyUrl,
                    TestUrl = x.TestUrl,
                    Database = x.Database,
                    DatabaseVersion = x.DatabaseVersion,
                    TestDatabase = x.TestDatabase,
                    WebServer = x.WebServer,
                    MvcVersion = x.MvcVersion,
                    AppPoolName = x.AppPoolName,
                    FilePath = x.FilePath,
                    TfsPath = x.TfsPath,
                    EndDateTime = x.EndDateTime,
                    DevIp = x.DevIp,
                    ProdIp = x.ProdIp


                }).ToList();

            return apps;
        }

        public List<int> GrabAdminUserAppIds(string blazerId)
        {
            List<int> usersAppIds = _localDB.UserApps.Where(x => x.BlazerId == blazerId && x.RoleId == 1).Select(x => x.AppId).ToList();

            return usersAppIds;
        }

        public List<AppModel> GrabAdminUserApps(string blazerId)
        {
            using (AppListDBContext _db = new AppListDBContext())
            {
                try
                {
                    List<int> userInstances = GrabAdminUserAppIds(blazerId);
                    List<AppModel> apps = _db.Applications.Where(x => userInstances.Contains(x.Id)).Select(x =>

                        new AppModel
                        {

                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description,
                            ProductionUrl = x.ProductionUrl,
                            FriendlyUrl = x.FriendlyUrl,
                            TestUrl = x.TestUrl,
                            Database = x.Database,
                            DatabaseVersion = x.DatabaseVersion,
                            TestDatabase = x.TestDatabase,
                            WebServer = x.WebServer,
                            MvcVersion = x.MvcVersion,
                            AppPoolName = x.AppPoolName,
                            FilePath = x.FilePath,
                            TfsPath = x.TfsPath,
                            EndDateTime = x.EndDateTime,
                            DevIp = x.DevIp,
                            ProdIp = x.ProdIp


                        }).ToList();

                    return apps;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public AppModel GrabSingleApp(int id, string blazerId)
        {
            var local = _localDB;
            var appdb = _db;

            //TODO: This method is jacked up. Its returning null when I try to make the call with blazerId as the param

            //the db context at the top of the page is always returning null as default need to figure this out

            //var user = _localDB.UserApps.Where(x => x.BlazerId == blazerId).FirstOrDefault();
            AppModel view = new AppModel();



            var selected = appdb.Applications.Find(id);



            view.Id = selected.Id;
            view.Name = selected.Name;
            view.Description = selected.Description;
            view.ProductionUrl = selected.ProductionUrl;
            view.FriendlyUrl = selected.FriendlyUrl;
            view.TestUrl = selected.TestUrl;
            view.Database = selected.Database;
            view.DatabaseVersion = selected.DatabaseVersion;
            view.TestDatabase = selected.TestDatabase;
            view.WebServer = selected.WebServer;
            view.MvcVersion = selected.MvcVersion;
            view.AppPoolName = selected.AppPoolName;
            view.FilePath = selected.FilePath;
            view.TfsPath = selected.TfsPath;
            view.EndDateTime = selected.EndDateTime;
            view.DevIp = selected.DevIp;
            view.ProdIp = selected.ProdIp;


            return view;


        }

        public Boolean CheckIfUserHasApp(int appId, string blazerId)
        {
            using (MyDbContext _db = new MyDbContext())
            {
                try
                {
                    return _db.UserApps.Any(x => x.AppId == appId && x.BlazerId == blazerId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public string GrabAppName(int appId)
        {
            using (AppListDBContext _db = new AppListDBContext())
            {
                try
                {
                    var application = _db.Applications.SingleOrDefault(x => x.Id == appId);
                    return application.Name;
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


//////Old Work here is a grab secondary grab usersapps if current doesnt work.

//public List<AppModel> Grabusersapps(string blazerId)
//{
//    List<AppModel> list = new List<AppModel>();
//    try
//    {
//        List<int> userInstances = GrabUsersAppIds(blazerId);
//        foreach (var Id in userInstances)
//        {
//            var app = _db.Applications.Single(x => x.Id == Id);
//            var appModel = new AppModel
//            {
//                Id = app.Id,
//                Name = app.Name
//            };
//            list.Add(appModel);
//        }
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e);
//        throw;
//    }
//    return list;
//}

//public AppModel GrabSingleApp(int id, string blazerId)
//{
//    var local = _localDB;
//    var appdb = _db;

//    

//   the db context at the top of the page is always returning null as default need to figure this out

//    var user = _localDB.UserApps.Where(x => x.BlazerId == blazerId).FirstOrDefault();

//    List<int> userInstances = GrabUsersAppIds(blazerId);

//    var selected = appdb.Applications.Find(id);
//    if (userInstances.Contains(selected.Id))
//    {
//        return new AppModel
//        {

//            Id = selected.Id,
//            Name = selected.Name,
//            Description = selected.Description,
//            ProductionUrl = selected.ProductionUrl,
//            FriendlyUrl = selected.FriendlyUrl,
//            TestUrl = selected.TestUrl,
//            Database = selected.Database,
//            DatabaseVersion = selected.DatabaseVersion,
//            TestDatabase = selected.TestDatabase,
//            WebServer = selected.WebServer,
//            MvcVersion = selected.MvcVersion,
//            AppPoolName = selected.AppPoolName,
//            FilePath = selected.FilePath,
//            TfsPath = selected.TfsPath,
//            EndDateTime = selected.EndDateTime,
//            DevIp = selected.DevIp,
//            ProdIp = selected.ProdIp


//        };
//    }

//    AppModel mod = new AppModel();
//    return mod;

//    }


