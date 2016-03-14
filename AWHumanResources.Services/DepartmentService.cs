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

        public DepartmentService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource;
        }

        public List<DepartmentDto> GetDepartments()
        {
            return m_DataSource.From("HumanResources.Department").ToCollection<DepartmentDto>().Execute();
        }
    }
}
