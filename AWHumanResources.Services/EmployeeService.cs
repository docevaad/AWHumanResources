using AWHumanResources.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace AWHumanResources.Services
{
    public class EmployeeService
    {
        private readonly SqlServerDataSource m_DataSource;

        public EmployeeService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource;
        }

        public List<EmployeeDto> GetAll()
        {
            return m_DataSource.From("HumanResources.vEmployeeDepartment").ToCollection<EmployeeDto>().Execute();
        }

        public List<EmployeeDto> GetEmployeesByDepartment(string department)
        {
            var sql = m_DataSource.From("HumanResources.vEmployeeDepartment", new { Department = department }).ToCollection<EmployeeDto>().Sql();
            Debug.WriteLine($"Sql: {sql}");
            return m_DataSource.From("HumanResources.vEmployeeDepartment", new { Department = department }).ToCollection<EmployeeDto>().Execute();
        }
    }
}
