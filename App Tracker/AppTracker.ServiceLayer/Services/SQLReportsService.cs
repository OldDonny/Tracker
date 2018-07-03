using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTracker.ServiceLayer.CommonObjectsService;
using AppTracker.ServiceLayer.Models;
using AppTracker.ServiceLayer.Models.OracleModel;
using AppTracker.ServiceLayer.Services.OracleConnection;
using AppTracker.ServiceLayer.WebService;

namespace AppTracker.ServiceLayer.Services
{

    //TODO: get sp to pass list of app ids to oracle and sql
    public class SQLReportsService
    {
        private AppService _appService = new AppService();
        private OracleFillService _orcl = new OracleFillService();
        private ApplcationsReportData _app = new ApplcationsReportData();

        public List<OracleModel> GetSQLReports(int Id)
        {
            var reports = _app.GetReportsForApplication(Id);

            return reports.AsEnumerable().Select(x => new OracleModel
            {
                ApplicationId = x.Field<int>("APPLICATION_ID"),

                ApplicationName = x.Field<string>("APPLICATION_NAME"),
                ReportName = x.Field<string>("REPORT_NAME"),
                ReportDescription = x.Field<string>("REPORT_DESCRIPTION"),
                ReportLinkDev = x.Field<string>("REPORT_LINK_PROD"),
                ReportType = x.Field<string>("REPORT_TYPE"),
                ReportGrouping = x.Field<string>("REPORT_GROUPING")


            }).ToList();

        }


        public List<OracleModel> ReturnAllUserReports(string blazerid)
        {
            List<OracleModel> combinedReports = new List<OracleModel>();
            var appIds = _appService.GrabUsersAppIds(blazerid);
            //need to find out if there is a stored procedure to return list of reports.
             

            var sql = GetSQLReports(0);
            var t = sql.Where(x => appIds.Contains(x.ApplicationId));
            combinedReports.AddRange(t);

            foreach (var id in appIds)
            {
                combinedReports.AddRange(_orcl.FillOracle(id));
            }

            return combinedReports;

        }
    }
}



