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


namespace AppTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private AppService _appService = new AppService();
        private ApplcationsReportData _reportService = new ApplcationsReportData();
        private OracleFillService _oracleService = new OracleFillService();

        public PartialViewResult ReportsTable(int? Id)
        {
            //reports table will be user to grab specific applications reports (need to write a new service that returns all of the users reports from database)
            List<ReportsViewModel> reportsList = new List<ReportsViewModel>();


            var reports = _oracleService.FillOracle(119);
            foreach (var report in reports)
            {

                ReportsViewModel vm = new ReportsViewModel();

                vm.ApplicationName = report.ApplicationName;
                vm.ProductionLink = report.ReportProdLink;
                vm.ReportDescription = report.ReportDescription;
                vm.ReportGrouping = report.ReportGrouping;
                vm.ReportName = report.ReportName;
                vm.ReportType = report.ReportType;
                reportsList.Add(vm);

            }


            return PartialView("_ReportsPartial", reportsList);

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