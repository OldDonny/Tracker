using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTracker.ServiceLayer.Models.OracleModel;

namespace AppTracker.ServiceLayer.Services.OracleConnection
{
    public class OracleFillService
    {
        private OracleConnection _orcl = new OracleConnection();
        public List<OracleModel> FillOracle(int? applicationId)
        {
            DataTable dt = _orcl.GetMatchingReports(applicationId);


            return dt.AsEnumerable().Select(x => new OracleModel
            {
                ApplicationId = x["APPLICATION_ID"] == null || string.IsNullOrEmpty(x["APPLICATION_ID"].ToString()) ? (int?)null : int.Parse(dt.Rows[0]["APPLICATION_ID"].ToString()),
                ApplicationName = x["APPLICATION_NAME"] == null || string.IsNullOrEmpty(x["APPLICATION_NAME"].ToString()) ? "" : x["APPLICATION_NAME"].ToString(),
                ReportName = x["REPORT_NAME"] == null || string.IsNullOrEmpty(x["REPORT_NAME"].ToString()) ? "" : x["REPORT_NAME"].ToString(),
                ReportDescription = x["REPORT_DESCRIPTION"] == null || string.IsNullOrEmpty(x["REPORT_DESCRIPTION"].ToString()) ? "" : x["REPORT_DESCRIPTION"].ToString(),
                ReportProdLink = x["REPORT_LINK_PROD"] == null || string.IsNullOrEmpty(x["REPORT_LINK_PROD"].ToString()) ? "" : x["REPORT_LINK_PROD"].ToString(),
                ReportType = x["REPORT_TYPE"] == null || string.IsNullOrEmpty(x["REPORT_TYPE"].ToString()) ? "" : x["REPORT_TYPE"].ToString(),
                ReportGrouping = x["REPORT_GROUPING"] == null || string.IsNullOrEmpty(x["REPORT_GROUPING"].ToString()) ? "" : x["REPORT_GROUPING"].ToString()
            }).ToList();

        }
    }
}
