using AWHumanResources.Data;
using AWHumanResources.Data.ViewModels;
using AWHumanResources.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AWHumanResources.Web.Controllers
{
    /// <summary>
    /// Department Controller
    /// </summary>
    /// <seealso cref="ApiController" />
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiController
    {
        private readonly DepartmentService m_DepartmentService;
        private readonly EmployeeService m_EmployeeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentController"/> class.
        /// </summary>
        /// <param name="departmentService">The department service.</param>
        /// <param name="employeeService">The employee service.</param>
        public DepartmentController(DepartmentService departmentService, EmployeeService employeeService)
        {
            m_DepartmentService = departmentService;
            m_EmployeeService = employeeService;
        }

        /// <summary>
        /// Gets a list of departments. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<List<DepartmentDto>> Get(CancellationToken cancellationToken = default(CancellationToken))
        {
            return m_DepartmentService.GetDepartmentsAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a department by ID.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{departmentId:int}")]
        public Task<DepartmentDto> Get(int departmentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return m_DepartmentService.GetDepartmentByIdAsync(departmentId, cancellationToken);
        }

        /// <summary>
        /// Gets a list of employees for the passed department ID.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{departmentId:int}/employees")]
        public Task<List<EmployeeViewVM>> GetByDepartment(int departmentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return m_EmployeeService.GetEmployeesByDepartmentIdAsync(departmentId, cancellationToken);
        }
    }
}