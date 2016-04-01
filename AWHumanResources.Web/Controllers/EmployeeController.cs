using AWHumanResources.Data.ViewModels;
using AWHumanResources.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AWHumanResources.Web.Controllers
{
    /// <summary>
    /// Employee Controller
    /// </summary>
    /// <seealso cref="ApiController" />
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeService m_EmployeeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeService">The employee service.</param>
        public EmployeeController(EmployeeService employeeService)
        {
            m_EmployeeService = employeeService;
        }

        /// <summary>
        /// Gets a list of employees in the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<List<EmployeeViewVM>> Get(CancellationToken cancellationToken = default(CancellationToken))
        {
            return m_EmployeeService.GetAllAsync();
        }

        /// <summary>
        /// Gets an employee by the passed identifier.
        /// </summary>
        /// <param name="employeeID">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{employeeID:int}")]
        public Task<EmployeeViewVM> Get(int employeeID, CancellationToken cancellationToken = default(CancellationToken))
        {
            return m_EmployeeService.GetEmployeeByIdAsync(employeeID, cancellationToken);
        }

        /// <summary>
        /// Updates an employee's pay.
        /// </summary>
        /// <param name="employeeID">The employee identifier.</param>
        /// <param name="updateRequest">The update request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{employeeID:int}/UpdatePay")]
        public Task<EmployeeViewVM> UpdatePay(int employeeID, EmpPayUpdateRequest updateRequest)
        {
            return m_EmployeeService.UpdateEmployeePayHistAsync(employeeID, updateRequest);
        }

        /// <summary>
        /// Updates an employee's dept.
        /// </summary>
        /// <param name="employeeID">The employee identifier.</param>
        /// <param name="updateRequest">The update request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{employeeID:int}/UpdateDept")]
        public Task<EmployeeViewVM> UpdateDept(int employeeID, EmpDeptUpdateRequest updateRequest)
        {
            return m_EmployeeService.UpdateEmployeeDepartmentAsync(employeeID, updateRequest);
        }
    }
}
