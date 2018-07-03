using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AppTracker.ServiceLayer.Models;
using AppTracker.ServiceLayer.Models.OracleModel;
using AppTracker.ServiceLayer.Services;
using AppTracker.ServiceLayer.Services.OracleConnection;


namespace AppTracker.ServiceLayer.CommonObjectsService
{
    public class ApplcationsReportData
    {
        OracleFillService _orcl = new OracleFillService();

        private string _connectionString;

        public ApplcationsReportData()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CommonObjects"].ToString();
        }

        /// <summary>
        /// Finds the Reports for a specific application
        ///
        /// This is meant to return the reports that are in sql server, there will be a web service to retrive the reports from Oracle.
        /// Query the UserApps table first to find out what applications are the loged in user has access to and then pass that value into GetReportsForApplications as the param
        /// TODO: write logic to determine if the applications report is stored in orcal db or in ApplicationsReports localed in sql server, maybe check to see if the application object has a report (bool) if no then return no report if yes either check sqlserver first and if that returns null then call the web service to oracle with the same param for AppID
        /// 
        /// 
        /// </summary>
        /// <param name="parmAppID"></param>
        /// <returns></returns>
        ///
        ///
        ///
        /// Will Probably have to extract some of this conditional logic out of the data layer and into the service layer.
        ///
        ///


        public DataTable GetReportsForApplication(int parmAppID)
        {
            //This service returns all sql reports and then they are filtered by blazerid the results will be filtered in other services due to there not being many reports in the db most are in oracle

            DataSet matchingReports = new DataSet();
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("spa_GetApplicationReports", conn);

            conn.Open();
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                var param0 = comm.Parameters.Add("parmAppID", SqlDbType.Int);
                param0.Direction = ParameterDirection.Input;
                var adapter = new SqlDataAdapter(comm);
                adapter.Fill(matchingReports);
                conn.Close();
                conn.Dispose();
                var tab = matchingReports.Tables[0];

                return tab;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            finally
            {
                conn.Close();
                conn.Dispose();
            }
 }


        //public List<OracleModel> GetDistinctReportsForApplication(int? parmAppID)
        //{
        //    //This service returns all sql reports and then they are filtered by blazerid the results will be filtered in other services due to there not being many reports in the db most are in oracle
     
        //        DataTable matchingReports = new DataTable();
        //        SqlConnection conn = new SqlConnection(_connectionString);
        //        SqlCommand comm = new SqlCommand("spa_GetApplicationReports", conn);
           
        //        conn.Open();
        //        comm.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        var param0 = comm.Parameters.Add("parmAppID", SqlDbType.Int);
        //        param0.Direction = ParameterDirection.Input;
        //        var reader = comm.ExecuteReader();
        //        matchingReports.Load(reader);
        //        conn.Close();
        //        conn.Dispose();
        //        var match = matchingReports.AsEnumerable().ToList().Where(x => x.Field<int>("APPLICATION_ID") == parmAppID);

        //        foreach (var ma in match)
        //        {
        //            if ((int)ma[0] == parmAppID)
        //            {
        //                return match.Select(x => new OracleModel
        //                {
        //                    ApplicationId = x.Field<int>("APPLICATION_ID"),
        //                    ApplicationName = x.Field<string>("APPLICATION_NAME"),
        //                    ReportName = x.Field<string>("REPORT_NAME"),
        //                    ReportDescription = x.Field<string>("REPORT_DESCRIPTION"),
        //                    ReportLinkDev = x.Field<string>("REPORT_LINK_PROD"),
        //                    ReportType = x.Field<string>("REPORT_TYPE"),
        //                    ReportGrouping = x.Field<string>("REPORT_GROUPING")


        //                }).ToList();

        //            }

        //        }



        //        if (reader.GetValue(0) == null || reader.GetValue(1) == null)

        //            return oracle.GetMatchingReports(parmAppID);
        //        else
        //        {
        //            var match = matchingReports.AsEnumerable().ToList().Where(x => x.Field<int>("APPLICATION_ID") == parmAppID);

        //            return match.Select(x => new OracleModel
        //            {
        //                ApplicationId = x.Field<int>("APPLICATION_ID"),
        //                ApplicationName = x.Field<string>("APPLICATION_NAME"),
        //                ReportName = x.Field<string>("REPORT_NAME"),
        //                ReportDescription = x.Field<string>("REPORT_DESCRIPTION"),
        //                ReportLinkDev = x.Field<string>("REPORT_LINK_PROD"),
        //                ReportType = x.Field<string>("REPORT_TYPE"),
        //                ReportGrouping = x.Field<string>("REPORT_GROUPING")


        //            }).ToList();

        //        }
                
        //    }
            

        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //    finally
        //    {
        //        conn.Close();
        //        conn.Dispose();
        //    }

        //}


        //AppService _appService = new AppService();
        //OracleConnection _oracleConnection = new OracleConnection();

        //public List<OracleModel> ReturnAllUserReports(string blazerid)
        //{
        //    List<OracleModel> combinedReports = new List<OracleModel>();
        //    var appIds = _appService.GrabUsersAppIds(blazerid);
        //    var sqlReports = GetReportsForApplication(0);

        //    need to find out if there is a stored procedure to return list of reports.


        //    List<OracleModel> sqlMatch = sqlReports.Where(x => appIds.Contains(x.ApplicationId)).ToList();


        //    foreach (var match in sqlMatch)
        //    {
        //        combinedReports.Add(match);

        //    }

        //    Not sure if this is the mosat effective way to get a list of oracle reports

        //    foreach (var id in appIds)
        //    {
        //        combinedReports.AddRange(_orcl.FillOracle(id));

        //    }

        //    return combinedReports;

        //}



    }
}

