using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using EmployeesManagerBL.Model;

namespace EmployeesManagerBL
{
    public static class EmployeesHandler
    {
        //запрос на изменение сотрудника
        public static void EditEmployee(int employeeId, Employee employee)
        {
            try
            {
                new EmployeesDatabaseDataSetTableAdapters.EmployeesTableAdapter().UpdateQuery(employee.FirstName,
                    employee.LastName, employee.Patronymic, employee.DateOfBirth.ToString(CultureInfo.InvariantCulture), employee.AddressOfResidence,
                    employee.Department, employee.AboutMe, employeeId);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        //запрос на добавление сотрудника
        public static void AddEmployee(Employee employee)
        {
            try
            {
                new EmployeesDatabaseDataSetTableAdapters.EmployeesTableAdapter().InsertQuery(employee.FirstName,
                    employee.LastName, employee.Patronymic, employee.DateOfBirth.ToString(CultureInfo.InvariantCulture), employee.AddressOfResidence,
                    employee.Department, employee.AboutMe);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        //запрос на получение всех сотрудников
        public static EmployeesDatabaseDataSet.EmployeesDataTable GetAllEmployees()
        {
            var employees = new EmployeesDatabaseDataSetTableAdapters.EmployeesTableAdapter().GetData();

            return employees;
        }
        //запрос на удаление сотрудника по его id
        public static void DeleteEmployeeById(int id)
        {
            try
            {
                new EmployeesDatabaseDataSetTableAdapters.EmployeesTableAdapter().DeleteQuery(id);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}