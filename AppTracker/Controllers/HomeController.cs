using System.Collections;
using AppTracker.ServiceLayer.Services;
using AppTracker.ServiceLayer.CommonObjectsService;
using AppTracker.ServiceLayer.Services.OracleConnection;
using AppTracker.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using AppTracker.ServiceLayer.CommonObjectsService;

namespace AppTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private AppService _appService = new AppService();
        private ApplcationsReportData _reportService = new ApplcationsReportData();
        private OracleConnection _oracleConnection = new OracleConnection();

        // Getting an error when reports table runs
        // the error is occuring in the GetApplicationsReports SService sating ParamAppId is not the param in spa_GetApplicationsReports
        // need to get with don and figure this out so I can view the data in the datatable



        //TODO: get edit user records service working so that it allows you to pass the list of role ids and appids and submits them to the db in the correct order the tyhe role matches the correct appid
        //TODO: set up session and lof out correcly so that it redirectas to either cas login or to uab.com/it/
        //TODO: check for lapses in security on login logout and also any random bugs that allow air into the application
        //TODO: we are handling the list of applications in the backend and may put the list in session state to preserve the variable
        //TODO: need to add links in the datatable for prod links in apps and reports
        public PartialViewResult ReportsTable(int? Id)
        {
            //reports table will be user to grab specific applications reports (need to write a new service that returns all of the users reports from database)
            List<ReportsViewModel> reportsList = new List<ReportsViewModel>();

            //var apps = _appService.GrabUsersAppIds(User.Identity.Name);90

            //var reports = _reportService.GetDistinctReportsForApplication(Id).Where(x=> apps.Contains(x.ApplicationId));

            var reports = _oracleConnection.GetMatchingReports(119);
            var tables = reports.Tables;

            return tables.AsEnumerable().ToList().Select(x => new OracleModel
            {
                ApplicationId = x.Field<int>("APPLICATION_ID"),
                ApplicationName = x.Field<string>("APPLICATION_NAME"),
                ReportName = x.Field<string>("REPORT_NAME"),
                ReportDescription = x.Field<string>("REPORT_DESCRIPTION"),
                ReportProdLink = x.Field<string>("REPORT_LINK_PROD"),
                ReportType = x.Field<string>("REPORT_TYPE"),
                ReportGrouping = x.Field<string>("REPORT_GROUPING")
            }).ToList();
            //foreach (var table in tables)
            //{

            //    ReportsViewModel vm = new ReportsViewModel();

            //    vm.ApplicationName = 
            //    vm.ProductionLink = report.ReportProdLink;
            //    vm.ReportDescription = report.ReportDescription;
            //    vm.ReportGrouping = report.ReportGrouping;
            //    vm.ReportName = report.ReportName;
            //    vm.ReportType = report.ReportType;
            //    reportsList.Add(vm);
            //}
            //return View(reportsList)
        }

        public PartialViewResult AllReports()
        {
            var user = User.Identity.Name;
            List<ReportsViewModel> allReportsList = new List<ReportsViewModel>();

            var usersReports = _reportService.ReturnAllUserReports(user);

            foreach (var report in usersReports)
            {
                ReportsViewModel vm = new ReportsViewModel();
            
                vm.ApplicationName = report.ApplicationName;
                vm.ProductionLink = report.ReportProdLink;
                vm.ReportDescription = report.ReportDescription;
                vm.ReportGrouping = report.ReportGrouping;
                vm.ReportName = report.ReportName;
                vm.ReportType = report.ReportType;
                allReportsList.Add(vm);
            }

            return PartialView("_ReportsPartial", allReportsList);

        }


        public PartialViewResult AppsDropdown()
        {
            //this partial view is for the _layout dropdown for all of the apps the user has access to

            List<AppPartialViewModel> model = new List<AppPartialViewModel>();
            var apps = _appService.Grabusersapps(User.Identity.Name);
            foreach (var app in apps)
            {
                AppPartialViewModel appmodel = new AppPartialViewModel();
                appmodel.Id = app.Id;
                appmodel.Name = app.Name;
                model.Add(appmodel);
        
            }

            return PartialView("_UserAppsPartial", model);
        }



        public ActionResult Index()
        {
            //this view will be for displaying all of the users applications and reports this view is relient on the ReportsTable patialview
        


            List<AppPartialViewModel> model = new List<AppPartialViewModel>();

            var apps = _appService.Grabusersapps(User.Identity.Name);

           
                foreach (var app in apps)
                {
                    AppPartialViewModel appmodel = new AppPartialViewModel();
                    appmodel.Id = app.Id;
                    appmodel.Name = app.Name;
                    appmodel.Description = app.Description;
                    appmodel.ProductionUrl = app.ProductionUrl;
                    appmodel.TestUrl = app.TestUrl;
                    appmodel.EndDateTime = app.EndDateTime;
                    model.Add(appmodel);
                }
            
           

            return View(model);
        }


        public ActionResult singleApp(int id)
        {
            AppPartialViewModel view = new AppPartialViewModel();
            //This controller will be used to show just the selected application in the datatable
            //var accessCheck = _appService.GrabUsersAppIds(User.Identity.Name);
            //var apps = _appService.Grabusersapps(User.Identity.Name);
            var app = _appService.GrabSingleApp(id, User.Identity.Name);

            if (app != null)
            {
                AppPartialViewModel vm = new AppPartialViewModel();
                vm.Id = app.Id;
                vm.Name = app.Name;
                vm.Description = app.Description;
                vm.ProductionUrl = app.ProductionUrl;
                vm.TestUrl = app.TestUrl;
                vm.EndDateTime = app.EndDateTime;

                return View(vm);
            }

            return View(view);

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}