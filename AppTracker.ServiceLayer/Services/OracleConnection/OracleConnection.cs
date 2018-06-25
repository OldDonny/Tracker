using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using AppTracker.ServiceLayer.Models.OracleModel;

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
        public DataSet GetMatchingReports(int? p_ApplicationID)
        {
            var con = ConfigurationManager.ConnectionStrings["OraReports"].ConnectionString;           
            Oracle.ManagedDataAccess.Client.OracleConnection connection = new Oracle.ManagedDataAccess.Client.OracleConnection(con);

            try
            {

              
                //TODO: if this doesnt work and i cant figure out the issue lets move to an unmanaged native driver but first try to set l;onger connection time out before doing that
                //The entry is missing from tnsnames.ora
                //The entry in tnsnames.ora is malformed
                //The program is using tnsnames.ora from the wrong ORACLE_HOME
                //The program is not using a fully-qualified service name, but no default domain is enabled in sqlnet.ora
             
                OracleCommand command = new OracleCommand("ZZUAB_WAM.wfuab_Get_Application_Reports", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.BindByName = true;

                command.Parameters.Add("p_ApplicationID", p_ApplicationID).OracleDbType = OracleDbType.Varchar2;
                command.Parameters.Add("p_recordSet ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataSet result = new DataSet();
                
                connection.Open();
                adapter.Fill(result);
                //var tabs = result.Tables;

                //foreach (var tab in tabs)
                //{
                    
                //}

                return result;

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


}
