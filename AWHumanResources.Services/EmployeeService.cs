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

        public List<EmployeeViewDto> GetAll()
        {
            return m_DataSource.From("HumanResources.vEmployeeWithPayHist")
                .ToCollection<EmployeeViewDto>()
                .Execute();
        }

        public List<EmployeeViewDto> GetEmployeesByDepartment(string department)
        {
            return m_DataSource.From("HumanResources.vEmployeeWithPayHist", new { DepartmentName = department })
                .ToCollection<EmployeeViewDto>()
                .Execute();
        }

        public List<EmployeeViewDto> GetEmployeeById(int id)
        {
            return m_DataSource.From("HumanResources.vEmployeeWithPayHist", new { BusinessEntityID = id })
                .ToCollection<EmployeeViewDto>()
                .Execute();
        }
    }
}
