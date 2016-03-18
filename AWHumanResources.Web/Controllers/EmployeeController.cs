using AWHumanResources.Data.ViewModels;
using AWHumanResources.Services;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var empViewDtos = m_EmployeeService.GetAll();
            if (empViewDtos == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewDtos); ;
        }

        [HttpGet]
        [Route("{department}")]
        public HttpResponseMessage Get(HttpRequestMessage request, string department)
        {
            var empViewDtos = m_EmployeeService.GetEmployeesByDepartment(department);

            if (empViewDtos == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var empViewDto = m_EmployeeService.GetEmployeeById(id);
            if (empViewDto == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewDto);
        }

        [HttpPost]
        [Route("UpdatePay")]
        public HttpResponseMessage Post(HttpRequestMessage request, EmpPayUpdateVM vm)
        {
            var empViewDto = m_EmployeeService.UpdateEmployeePayHist(vm);
            if (empViewDto == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewDto);
        }

        [HttpPost]
        [Route("UpdateDept")]
        public HttpResponseMessage Post(HttpRequestMessage request, EmpDeptUpdateVM vm)
        {
            var empViewDto = m_EmployeeService.UpdateEmployeeDepartment(vm);
            if (empViewDto == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewDto);
        }
    }
}
