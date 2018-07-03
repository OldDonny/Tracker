using AppTracker
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppTracker.Service.CommonObjectsService
{
    public class EmployeeService
    {
        private EmployeeData _employeeData = new EmployeeData();
        ExceptionService _exceptionService = new ExceptionService();

        /// <summary>
        /// Finds employees with supervisor data by name, blazerId, or employee number
        /// </summary>
        /// <param name="nameBlazerIdEmployeeNo"></param>
        /// <returns></returns>
        public List<EmployeeModel> EmployeeSearch(string nameBlazerIdEmployeeNo)
        {
            try
            {
                DataTable dt = _employeeData.EmployeeSearch(nameBlazerIdEmployeeNo);

                List<EmployeeModel> employees = dt.AsEnumerable().Select(x => new EmployeeModel
                {
                    EmployeeNumber = x["EMPLOYEE_NUMBER"] == null ? "" : x["EMPLOYEE_NUMBER"].ToString(),
                    FullName = x["FULL_NAME"] == null ? "" : x["FULL_NAME"].ToString(),
                    LastName = x["LAST_NAME"] == null ? "" : x["LAST_NAME"].ToString(),
                    FirstName = x["FIRST_NAME"] == null ? "" : x["FIRST_NAME"].ToString(),
                    MiddleName = x["MIDDLE_NAME"] == null ? "" : x["MIDDLE_NAME"].ToString(),
                    BlazerId = x["BLAZERID"] == null ? "" : x["BLAZERID"].ToString(),
                    EmailAddress = x["EMAIL_ADDRESS"] == null ? "" : x["EMAIL_ADDRESS"].ToString(),
                    Job = x["JOB"] == null ? "" : x["JOB"].ToString(),
                    OrganizationName = x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" : x["ASSIGNMENT_ORGANIZATION_NAME"].ToString(),
                    Phone = x["CAMPUS_PHONE_NUMBER"] == null ? "" : x["CAMPUS_PHONE_NUMBER"].ToString(),
                    SupervisorFullName = x["SUPERVISOR_FULL_NAME"] == null ? "" : x["SUPERVISOR_FULL_NAME"].ToString(),
                    SupervisorBlazerId = x["SUPERVISOR_BLAZERID"] == null ? "" : x["SUPERVISOR_BLAZERID"].ToString(),
                    SupervisorEmployeeNumber = x["SUPERVISOR_EMPLOYEE_NUMBER"] == null ? "" : x["SUPERVISOR_EMPLOYEE_NUMBER"].ToString(),
                    OrganizationCode =
                        string.IsNullOrEmpty(x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" :
                        x["ASSIGNMENT_ORGANIZATION_NAME"].ToString()) || x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" :
                            x["ASSIGNMENT_ORGANIZATION_NAME"].ToString().Length < 9 ? null :
                            x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" :
                        x["ASSIGNMENT_ORGANIZATION_NAME"].ToString().Substring(0, 9)
                }).ToList();
                return employees ?? new List<EmployeeModel>();
            }
            catch (Exception ex)
            {
                _exceptionService.WriteException(ex);
                return new List<EmployeeModel>();
            }
        }

        /// <summary>
        /// Gets an employee with supervisor data by blazerId
        /// </summary>
        /// <param name="blazerId"></param>
        /// <returns></returns>
        public EmployeeModel GetEmployeeWithSupervisor(string blazerId)
        {
            try
            {
                DataTable dt = _employeeData.GetEmployeeWithSupervisor(blazerId);

                EmployeeModel employee = dt.AsEnumerable().Select(x => new EmployeeModel
                {
                    EmployeeNumber = x["EMPLOYEE_NUMBER"] == null ? "" : x["EMPLOYEE_NUMBER"].ToString(),
                    FullName = x["FULL_NAME"] == null ? "" : x["FULL_NAME"].ToString(),
                    LastName = x["LAST_NAME"] == null ? "" : x["LAST_NAME"].ToString(),
                    FirstName = x["FIRST_NAME"] == null ? "" : x["FIRST_NAME"].ToString(),
                    MiddleName = x["MIDDLE_NAME"] == null ? "" : x["MIDDLE_NAME"].ToString(),
                    BlazerId = x["BLAZERID"] == null ? "" : x["BLAZERID"].ToString(),
                    EmailAddress = x["EMAIL_ADDRESS"] == null ? "" : x["EMAIL_ADDRESS"].ToString(),
                    Job = x["JOB"] == null ? "" : x["JOB"].ToString(),
                    OrganizationName = x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" : x["ASSIGNMENT_ORGANIZATION_NAME"].ToString(),
                    Phone = x["CAMPUS_PHONE_NUMBER"] == null ? "" : x["CAMPUS_PHONE_NUMBER"].ToString(),
                    SupervisorFullName = x["SUPERVISOR_FULL_NAME"] == null ? "" : x["SUPERVISOR_FULL_NAME"].ToString(),
                    SupervisorBlazerId = x["SUPERVISOR_BLAZERID"] == null ? "" : x["SUPERVISOR_BLAZERID"].ToString(),
                    SupervisorEmployeeNumber = x["SUPERVISOR_EMPLOYEE_NUMBER"] == null ? "" : x["SUPERVISOR_EMPLOYEE_NUMBER"].ToString(),
                    OrganizationCode =
                        string.IsNullOrEmpty(x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" :
                        x["ASSIGNMENT_ORGANIZATION_NAME"].ToString()) || x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" :
                            x["ASSIGNMENT_ORGANIZATION_NAME"].ToString().Length < 9 ? null :
                            x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" :
                        x["ASSIGNMENT_ORGANIZATION_NAME"].ToString().Substring(0, 9)
                }).FirstOrDefault();
                return employee ?? new EmployeeModel() { BlazerId = blazerId };
            }
            catch (Exception ex)
            {
                _exceptionService.WriteException(ex);
                return new EmployeeModel() { BlazerId = blazerId };
            }
        }

        /// <summary>
        /// Lookups an employee by blazerId
        /// </summary>
        /// <param name="BlazerId"></param>
        /// <returns></returns>
        public string EmployeeNameLookup(string BlazerId)
        {
            try
            {
                DataTable dt = _employeeData.EmployeeSearch(BlazerId);
                EmployeeModel emp;
                String empName;
                emp = dt.AsEnumerable().Select(x => new EmployeeModel
                {
                    FullName = x["FULL_NAME"].ToString(),
                }).FirstOrDefault();
                empName = emp.FullName;
                return empName;
            }
            catch (Exception ex)
            {
                _exceptionService.WriteException(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets a list of employees for provided supervisor blazerId
        /// </summary>
        /// <param name="blazerId"></param>
        /// <returns></returns>
        public List<EmployeeModel> GetSupervisorEmployees(string blazerId)
        {
            try
            {
                DataTable dt = _employeeData.GetSupervisorEmployees(blazerId);

                List<EmployeeModel> employees = dt.AsEnumerable().Select(x => new EmployeeModel
                {
                    EmployeeNumber = x["EMPLOYEE_NUMBER"] == null ? "" : x["EMPLOYEE_NUMBER"].ToString(),
                    FullName = x["FULL_NAME"] == null ? "" : x["FULL_NAME"].ToString(),
                    LastName = x["LAST_NAME"] == null ? "" : x["LAST_NAME"].ToString(),
                    FirstName = x["FIRST_NAME"] == null ? "" : x["FIRST_NAME"].ToString(),
                    MiddleName = x["MIDDLE_NAME"] == null ? "" : x["MIDDLE_NAME"].ToString(),
                    BlazerId = x["BLAZERID"] == null ? "" : x["BLAZERID"].ToString(),
                    EmailAddress = x["EMAIL_ADDRESS"] == null ? "" : x["EMAIL_ADDRESS"].ToString(),
                    Job = x["JOB"] == null ? "" : x["JOB"].ToString(),
                    OrganizationName = x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" : x["ASSIGNMENT_ORGANIZATION_NAME"].ToString(),
                    Phone = x["CAMPUS_PHONE_NUMBER"] == null ? "" : x["CAMPUS_PHONE_NUMBER"].ToString()
                }).ToList();
                return employees ?? new List<EmployeeModel>();
            }
            catch (Exception ex)
            {
                _exceptionService.WriteException(ex);
                return new List<EmployeeModel>();
            }
        }

        /// <summary>
        /// Finds employees with by name or blazerId
        /// </summary>
        /// <param name="parmName"></param>
        /// <param name="parmBlazerIDEmpNo"></param>
        /// <returns></returns>
        public List<EmployeeModel> GetEmployeesByFilter(string parmName, string parmBlazerIDEmpNo)
        {
            try
            {
                DataTable dt = _employeeData.GetEmployeesByFilter(parmName, parmBlazerIDEmpNo);

                List<EmployeeModel> employees = dt.AsEnumerable().Select(x => new EmployeeModel
                {
                    EmployeeNumber = x["EMPLOYEE_NUMBER"].ToString().Trim(),
                    FullName = x["FULL_NAME"] == null ? "" : x["FULL_NAME"].ToString().Trim(),
                    LastName = x["LAST_NAME"] == null ? "" : x["LAST_NAME"].ToString().Trim(),
                    FirstName = x["FIRST_NAME"] == null ? "" : x["FIRST_NAME"].ToString().Trim(),
                    MiddleName = x["MIDDLE_NAME"] == null ? "" : x["MIDDLE_NAME"].ToString().Trim(),
                    BlazerId = x["BLAZERID"] == null ? "" : x["BLAZERID"].ToString().Trim(),
                    AssignCat = x["ASSIGNMENT_CATEGORY"] == null ? "" : x["ASSIGNMENT_CATEGORY"].ToString().Trim(),
                    EmailAddress = x["EMAIL_ADDRESS"] == null ? "" : x["EMAIL_ADDRESS"].ToString().Trim(),
                    Job = x["JOB"] == null ? "" : x["JOB"].ToString().Trim(),
                    OrganizationCode = x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" : x["ASSIGNMENT_ORGANIZATION_NAME"].ToString().Trim().Substring(0, 9),
                    OrganizationName = x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" : x["ASSIGNMENT_ORGANIZATION_NAME"].ToString().Trim(),
                    Phone = x["CAMPUS_PHONE_NUMBER"] == null ? "" : x["CAMPUS_PHONE_NUMBER"].ToString().Trim(),
                    SupervisorFullName = x["Supervisor"] == null ? "" : x["Supervisor"].ToString().Trim(),
                    SupervisorPhone = x["SupervisorPhone"] == null ? "" : x["SupervisorPhone"].ToString().Trim(),
                    SupervisorBlazerId = x["SUPERVISOR_BLAZERID"] == null ? "" : x["SUPERVISOR_BLAZERID"].ToString().Trim(),
                    SupervisorEmployeeNumber = x["SupervisorEmpNumber"] == null ? "" : x["SupervisorEmpNumber"].ToString().Trim(),
                    HomeAddress1 = x["HOME_ADDRESS_LINE1"] == null || string.IsNullOrEmpty(x["HOME_ADDRESS_LINE1"].ToString()) ? "" : x["HOME_ADDRESS_LINE1"].ToString().Trim(),
                    HomeAddress2 = x["HOME_ADDRESS_LINE2"] == null || string.IsNullOrEmpty(x["HOME_ADDRESS_LINE2"].ToString()) ? "" : x["HOME_ADDRESS_LINE2"].ToString().Trim(),
                    HomeAddress3 = x["HOME_ADDRESS_LINE3"] == null || string.IsNullOrEmpty(x["HOME_ADDRESS_LINE3"].ToString()) ? "" : x["HOME_ADDRESS_LINE3"].ToString().Trim(),
                    HomeTownCity = x["HOME_TOWN_OR_CITY"] == null || string.IsNullOrEmpty(x["HOME_TOWN_OR_CITY"].ToString()) ? "" : x["HOME_TOWN_OR_CITY"].ToString().Trim(),
                    HomeState = x["HOME_STATE"] == null || string.IsNullOrEmpty(x["HOME_STATE"].ToString()) ? "" : x["HOME_STATE"].ToString().Trim(),
                    HomeCountry = x["HOME_COUNTRY"] == null || string.IsNullOrEmpty(x["HOME_COUNTRY"].ToString()) ? "" : x["HOME_COUNTRY"].ToString().Trim(),
                    HomePostalCode = x["HOME_POSTAL_CODE"] == null || string.IsNullOrEmpty(x["HOME_POSTAL_CODE"].ToString()) ? "" : x["HOME_POSTAL_CODE"].ToString().Trim(),
                    HomePhone = x["HOME_PHONE_NUMBER"] == null || string.IsNullOrEmpty(x["HOME_PHONE_NUMBER"].ToString()) ? "" : x["HOME_PHONE_NUMBER"].ToString().Trim(),
                    CampusAddress1 = x["CAMPUS_ADDRESS_LINE1"] == null || string.IsNullOrEmpty(x["CAMPUS_ADDRESS_LINE1"].ToString()) ? "" : x["CAMPUS_ADDRESS_LINE1"].ToString().Trim(),
                    CampusAddress2 = x["CAMPUS_ADDRESS_LINE2"] == null || string.IsNullOrEmpty(x["CAMPUS_ADDRESS_LINE2"].ToString()) ? "" : x["CAMPUS_ADDRESS_LINE2"].ToString().Trim(),
                    CampusAddress3 = x["CAMPUS_ADDRESS_LINE3"] == null || string.IsNullOrEmpty(x["CAMPUS_ADDRESS_LINE3"].ToString()) ? "" : x["CAMPUS_ADDRESS_LINE3"].ToString().Trim(),
                    CampusTownCity = x["CAMPUS_TOWN_OR_CITY"] == null || string.IsNullOrEmpty(x["CAMPUS_TOWN_OR_CITY"].ToString()) ? "" : x["CAMPUS_TOWN_OR_CITY"].ToString().Trim(),
                    CampusState = x["CAMPUS_STATE"] == null || string.IsNullOrEmpty(x["CAMPUS_STATE"].ToString()) ? "" : x["CAMPUS_STATE"].ToString().Trim(),
                    CampusPostalCode = x["CAMPUS_POSTAL_CODE"] == null || string.IsNullOrEmpty(x["CAMPUS_POSTAL_CODE"].ToString()) ? "" : x["CAMPUS_POSTAL_CODE"].ToString().Trim(),
                    CampusPhone = x["CAMPUS_PHONE_NUMBER"] == null || string.IsNullOrEmpty(x["CAMPUS_PHONE_NUMBER"].ToString()) ? "" : x["CAMPUS_PHONE_NUMBER"].ToString().Trim(),
                    SupervisorJob = x["Supervisor_Job"] == null ? "" : x["Supervisor_Job"].ToString().Trim(),
                    SupervisorOrganizationCode = x["Supervisor_Assignment_Organization_Name"] == null ? "" : x["Supervisor_Assignment_Organization_Name"].ToString().Trim().Substring(0, 9),
                    SupervisorOrganizationName = x["Supervisor_Assignment_Organization_Name"] == null ? "" : x["Supervisor_Assignment_Organization_Name"].ToString().Trim(),
                }).ToList();
                return employees ?? new List<EmployeeModel>();
            }
            catch (Exception ex)
            {
                _exceptionService.WriteException(ex);
                return new List<EmployeeModel>();
            }
        }

        /// <summary>
        /// /// Finds employee info for a list of blazerIds
        /// </summary>
        /// <param name="parmBlazerIDEmpNoList"></param>
        /// <returns></returns>
        public List<EmployeeModel> GetEmployeeInfoByList(List<string> parmBlazerIDEmpNoList)
        {
            try
            {
                DataTable dt = _employeeData.GetEmployeeInfoByList(string.Join(",", parmBlazerIDEmpNoList));

                List<EmployeeModel> employees = dt.AsEnumerable().Select(x => new EmployeeModel
                {
                    EmployeeNumber = x["EMPLOYEE_NUMBER"].ToString().Trim(),
                    FullName = x["FULL_NAME"] == null ? "" : x["FULL_NAME"].ToString().Trim(),
                    LastName = x["LAST_NAME"] == null ? "" : x["LAST_NAME"].ToString().Trim(),
                    FirstName = x["FIRST_NAME"] == null ? "" : x["FIRST_NAME"].ToString().Trim(),
                    MiddleName = x["MIDDLE_NAME"] == null ? "" : x["MIDDLE_NAME"].ToString().Trim(),
                    BlazerId = x["BLAZERID"] == null ? "" : x["BLAZERID"].ToString().Trim(),
                    AssignCat = x["ASSIGNMENT_CATEGORY"] == null ? "" : x["ASSIGNMENT_CATEGORY"].ToString().Trim(),
                    EmailAddress = x["EMAIL_ADDRESS"] == null ? "" : x["EMAIL_ADDRESS"].ToString().Trim(),
                    Job = x["JOB"] == null ? "" : x["JOB"].ToString().Trim(),
                    OrganizationCode = x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" : x["ASSIGNMENT_ORGANIZATION_NAME"].ToString().Trim().Substring(0, 9),
                    OrganizationName = x["ASSIGNMENT_ORGANIZATION_NAME"] == null ? "" : x["ASSIGNMENT_ORGANIZATION_NAME"].ToString().Trim(),
                    Phone = x["CAMPUS_PHONE_NUMBER"] == null ? "" : x["CAMPUS_PHONE_NUMBER"].ToString().Trim(),
                    SupervisorFullName = x["Supervisor"] == null ? "" : x["Supervisor"].ToString().Trim(),
                    SupervisorPhone = x["SupervisorPhone"] == null ? "" : x["SupervisorPhone"].ToString().Trim(),
                    SupervisorBlazerId = x["SUPERVISOR_BLAZERID"] == null ? "" : x["SUPERVISOR_BLAZERID"].ToString().Trim(),
                    SupervisorEmployeeNumber = x["SupervisorEmpNumber"] == null ? "" : x["SupervisorEmpNumber"].ToString().Trim(),
                    HomeAddress1 = x["HOME_ADDRESS_LINE1"] == null || string.IsNullOrEmpty(x["HOME_ADDRESS_LINE1"].ToString()) ? "" : x["HOME_ADDRESS_LINE1"].ToString().Trim(),
                    HomeAddress2 = x["HOME_ADDRESS_LINE2"] == null || string.IsNullOrEmpty(x["HOME_ADDRESS_LINE2"].ToString()) ? "" : x["HOME_ADDRESS_LINE2"].ToString().Trim(),
                    HomeAddress3 = x["HOME_ADDRESS_LINE3"] == null || string.IsNullOrEmpty(x["HOME_ADDRESS_LINE3"].ToString()) ? "" : x["HOME_ADDRESS_LINE3"].ToString().Trim(),
                    HomeTownCity = x["HOME_TOWN_OR_CITY"] == null || string.IsNullOrEmpty(x["HOME_TOWN_OR_CITY"].ToString()) ? "" : x["HOME_TOWN_OR_CITY"].ToString().Trim(),
                    HomeState = x["HOME_STATE"] == null || string.IsNullOrEmpty(x["HOME_STATE"].ToString()) ? "" : x["HOME_STATE"].ToString().Trim(),
                    HomeCountry = x["HOME_COUNTRY"] == null || string.IsNullOrEmpty(x["HOME_COUNTRY"].ToString()) ? "" : x["HOME_COUNTRY"].ToString().Trim(),
                    HomePostalCode = x["HOME_POSTAL_CODE"] == null || string.IsNullOrEmpty(x["HOME_POSTAL_CODE"].ToString()) ? "" : x["HOME_POSTAL_CODE"].ToString().Trim(),
                    HomePhone = x["HOME_PHONE_NUMBER"] == null || string.IsNullOrEmpty(x["HOME_PHONE_NUMBER"].ToString()) ? "" : x["HOME_PHONE_NUMBER"].ToString().Trim(),
                    CampusAddress1 = x["CAMPUS_ADDRESS_LINE1"] == null || string.IsNullOrEmpty(x["CAMPUS_ADDRESS_LINE1"].ToString()) ? "" : x["CAMPUS_ADDRESS_LINE1"].ToString().Trim(),
                    CampusAddress2 = x["CAMPUS_ADDRESS_LINE2"] == null || string.IsNullOrEmpty(x["CAMPUS_ADDRESS_LINE2"].ToString()) ? "" : x["CAMPUS_ADDRESS_LINE2"].ToString().Trim(),
                    CampusAddress3 = x["CAMPUS_ADDRESS_LINE3"] == null || string.IsNullOrEmpty(x["CAMPUS_ADDRESS_LINE3"].ToString()) ? "" : x["CAMPUS_ADDRESS_LINE3"].ToString().Trim(),
                    CampusTownCity = x["CAMPUS_TOWN_OR_CITY"] == null || string.IsNullOrEmpty(x["CAMPUS_TOWN_OR_CITY"].ToString()) ? "" : x["CAMPUS_TOWN_OR_CITY"].ToString().Trim(),
                    CampusState = x["CAMPUS_STATE"] == null || string.IsNullOrEmpty(x["CAMPUS_STATE"].ToString()) ? "" : x["CAMPUS_STATE"].ToString().Trim(),
                    CampusPostalCode = x["CAMPUS_POSTAL_CODE"] == null || string.IsNullOrEmpty(x["CAMPUS_POSTAL_CODE"].ToString()) ? "" : x["CAMPUS_POSTAL_CODE"].ToString().Trim(),
                    CampusPhone = x["CAMPUS_PHONE_NUMBER"] == null || string.IsNullOrEmpty(x["CAMPUS_PHONE_NUMBER"].ToString()) ? "" : x["CAMPUS_PHONE_NUMBER"].ToString().Trim(),
                    SupervisorJob = x["Supervisor_Job"] == null ? "" : x["Supervisor_Job"].ToString().Trim(),
                    SupervisorOrganizationCode = x["Supervisor_Assignment_Organization_Name"] == null ? "" : x["Supervisor_Assignment_Organization_Name"].ToString().Trim().Substring(0, 9),
                    SupervisorOrganizationName = x["Supervisor_Assignment_Organization_Name"] == null ? "" : x["Supervisor_Assignment_Organization_Name"].ToString().Trim(),
                }).ToList();
                return employees ?? new List<EmployeeModel>();
            }
            catch (Exception ex)
            {
                _exceptionService.WriteException(ex);
                return new List<EmployeeModel>();
            }
        }
    }
}