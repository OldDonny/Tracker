using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppTracker.DataConnection.CommonObjects

{
    public class EmployeeData
    {
        private string _connectionString;

        public EmployeeData()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CommonObjects"].ToString();
        }

        /// <summary>
        /// Finds employees with supervisor data by name, blazerId, or employee number
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public DataTable EmployeeSearch(string search)
        {
            DataTable searchResultsTable = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("spa_EmployeeSearch", conn);

            conn.Open();
            comm.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter param0 = comm.Parameters.Add("parmSearch", SqlDbType.VarChar, 50);
                param0.Direction = ParameterDirection.Input;
                param0.Value = search;

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

        /// <summary>
        /// Gets an employee with supervisor data by blazerId
        /// </summary>
        /// <param name="blazerId"></param>
        /// <returns></returns>
        public DataTable GetEmployeeWithSupervisor(string blazerId)
        {
            DataTable employeeTable = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("spa_GetEmployeeWithSupervisor", conn);

            conn.Open();
            comm.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter param0 = comm.Parameters.Add("parmBlazerID", SqlDbType.VarChar, 10);
                param0.Direction = ParameterDirection.Input;
                param0.Value = blazerId;

                employeeTable.Load(comm.ExecuteReader());
            }
            catch (Exception e)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return employeeTable;
        }

        /// <summary>
        /// Gets a list of employees for provided supervisor blazerId
        /// </summary>
        /// <param name="blazerId"></param>
        /// <returns></returns>
        public DataTable GetSupervisorEmployees(string blazerId)
        {
            DataTable resultsTable = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("spa_GetSupervisorEmployees", conn);

            conn.Open();
            comm.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter param0 = comm.Parameters.Add("parmSuperBlazerId", SqlDbType.VarChar, 10);
                param0.Direction = ParameterDirection.Input;
                param0.Value = blazerId;

                resultsTable.Load(comm.ExecuteReader());
            }
            catch (Exception e)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return resultsTable;
        }

        /// <summary>
        /// Finds employees with by name or blazerId
        /// </summary>
        /// <param name="parmName"></param>
        /// <param name="parmBlazerIDEmpNo"></param>
        /// <returns></returns>
        public DataTable GetEmployeesByFilter(string parmName, string parmBlazerIDEmpNo)
        {
            DataTable searchResultsTable = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("spa_GetEmployeesByFilter", conn);

            conn.Open();
            comm.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter param0 = comm.Parameters.Add("parmName", SqlDbType.VarChar, 5);
                param0.Direction = ParameterDirection.Input;
                //param0.Value = DataUtilities.CheckForNull(parmName);

                SqlParameter param1 = comm.Parameters.Add("parmBlazerIDEmpNo", SqlDbType.VarChar, 255);
                param1.Direction = ParameterDirection.Input;
                //param1.Value = DataUtilities.CheckForNull(parmBlazerIDEmpNo);

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

        /// <summary>
        /// Finds employee info for a list of blazerIds
        /// </summary>
        /// <param name="parmBlazerIDList"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfoByList(string parmBlazerIDList)
        {
            DataTable searchResultsTable = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("spa_GetEmployeeInfoByList", conn);

            conn.Open();
            comm.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter param0 = comm.Parameters.Add("parmBlazerIDList", SqlDbType.NVarChar);
                param0.Direction = ParameterDirection.Input;
                //param0.Value = DataUtilities.CheckForNull(parmBlazerIDList);

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
