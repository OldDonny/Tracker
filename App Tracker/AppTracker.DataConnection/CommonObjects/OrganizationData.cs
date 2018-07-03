using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AppTracker.Data.CommonObjects
{
    public class OrganizationData
    {
        private string _connectionString;

        public OrganizationData()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CommonObjects"].ToString();
        }

        public DataTable GetOrganizationsByFilter(string filter)
        {
            DataTable searchResultsTable = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("spa_GetOrganizationsByFilter", conn);

            conn.Open();
            comm.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter param0 = comm.Parameters.Add("parmFilter", SqlDbType.VarChar, 15);
                param0.Direction = ParameterDirection.Input;
                param0.Value = filter;

                searchResultsTable.Load(comm.ExecuteReader());
            }
            catch (Exception e)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return searchResultsTable;
        }
    }
}
