using AWHumanResources.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace AWHumanResources.Services
{
    /// <summary>
    /// Department Service
    /// </summary>
    public class DepartmentService
    {
        private readonly SqlServerDataSource m_DataSource;
        private const string m_DeptTableName = "HumanResources.Department";

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentService"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public DepartmentService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource;
        }

        /// <summary>
        /// Gets a list of departments.
        /// </summary>
        /// <returns></returns>
        public Task<List<DepartmentDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return m_DataSource.From(m_DeptTableName).ToCollection<DepartmentDto>().ExecuteAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a department by identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public Task<DepartmentDto> GetDepartmentByIdAsync(int departmentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return m_DataSource.From(m_DeptTableName, new { DepartmentID = departmentId }).ToObject<DepartmentDto>().ExecuteAsync(cancellationToken);
        }
    }
}
