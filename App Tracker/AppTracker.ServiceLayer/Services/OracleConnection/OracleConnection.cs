using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Services;


namespace AppTracker.ServiceLayer.Services.OracleConnection
{
    [WebService]
    public class OracleConnection
    {

        /// 
        /// 
        /// </summary>
        /// <param name="parmAppID"></param>
        /// <returns></returns>
        ///
        ///
        public DataTable GetMatchingReports(int? p_ApplicationID)
        {
            var con = ConfigurationManager.ConnectionStrings["OraReports"].ConnectionString;
            Oracle.ManagedDataAccess.Client.OracleConnection connection = new Oracle.ManagedDataAccess.Client.OracleConnection(con);

            try
            {


                OracleCommand command = new OracleCommand("ZZUAB_WAM.wfuab_Get_Application_Reports", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.BindByName = true;



                command.Parameters.Add("p_ApplicationID", p_ApplicationID).OracleDbType = OracleDbType.Varchar2;
                command.Parameters.Add("p_recordSet ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataSet result = new DataSet();
                DataTable dt = new DataTable();
                connection.Open();
                adapter.Fill(result);


                var fill = result.Tables[0];


                return fill;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }
        }


    }

    public class Dataset : DataSet
    {
    }
}
