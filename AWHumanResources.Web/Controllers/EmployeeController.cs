using AWHumanResources.Data;
using AWHumanResources.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace AWHumanResources.Web.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeService m_EmployeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            m_EmployeeService = employeeService;
        }

        [HttpGet]
        public List<EmployeeViewDto> Get()
        {
            return m_EmployeeService.GetAll();
        }

        [HttpGet]
        [Route("{department}")]
        public List<EmployeeViewDto> Get(string department)
        {
            return m_EmployeeService.GetEmployeesByDepartment(department);
        }

        [HttpGet]
        [Route("{id:int}")]
        public List<EmployeeViewDto> Get(int id)
        {
            return m_EmployeeService.GetEmployeeById(id);
        }
    }
}
