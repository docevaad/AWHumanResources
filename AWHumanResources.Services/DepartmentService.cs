using AWHumanResources.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace AWHumanResources.Services
{
    public class DepartmentService
    {
        private readonly SqlServerDataSource m_DataSource;
        private readonly string m_DeptTableName = "HumanResources.Department";

        public DepartmentService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource;
        }

        public List<DepartmentDto> GetDepartments()
        {
            return m_DataSource.From(m_DeptTableName).ToCollection<DepartmentDto>().Execute();
        }

        public DepartmentDto GetDepartmentById(int departmentId)
        {
            return m_DataSource.From(m_DeptTableName, new { DepartmentID = departmentId }).ToObject<DepartmentDto>().Execute();
        }
    }
}
