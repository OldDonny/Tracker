using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace AppTracker.ServiceLayer.WebService
{
    class OracleService
    {
        //should I make the type a DataTable or string? If its a string I will have to convert to json and then parse in the controller.
        public DataTable GetMatchingReports()
        {
            OracleConnection dbconn = new OracleConnection("DataSource= , Password= , PersistSecurity= true , UserID= ");

            try
            {
                var strQry = @"pass in app id to stored procedure in oracle or local query depending on security and access";


                dbconn.Open();

                OracleCommand dbcomm = new OracleCommand(strQry, dbconn);
                OracleDataAdapter adapter = new OracleDataAdapter(dbcomm);
                DataTable result = new DataTable();

                adapter.Fill(result);

                dbconn.Close();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                dbconn.Close();
            }
        }


        //Ramya's implmentation of oracle data connection for reference using params
        //public DataTable GetMatchingReports2()
        //{
        //    OracleConnection con = new OracleConnection();
        //    con.ConnectionString = db.Database.Connection.ConnectionString;
        //    con.Open();
        //    OracleCommand cmd = new OracleCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = "";
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.Parameters.Add("o_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //    OracleDataAdapter da = new OracleDataAdapter(cmd);
        //    da.Fill(ds);
        //    DataRowCollection gg = ds.Rows;
        //    var d = ds.AsEnumerable();
        //    cmd.Dispose();
        //    con.Close();
        //    return d;
        //}
    }


}
