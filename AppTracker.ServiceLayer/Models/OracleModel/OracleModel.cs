using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTracker.ServiceLayer.Models.OracleModel
{
   public class OracleModel
    {
        //Will have to get all the feilds returned from oracle and create a model to accept them, then in oracle service we will grab the report and retrun a model with the data in it.
        //Should be able to return a Oracle model out of the oracle service.

        public int? ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public string ReportProdLink { get; set; }
        public string ReportType { get; set; }
        public string ReportGrouping { get; set; }
    }
}
