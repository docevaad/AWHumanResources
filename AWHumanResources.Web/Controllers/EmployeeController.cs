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
            var empViewVM = m_EmployeeService.GetAll();
            if (empViewVM == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewVM);
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var empViewVM = m_EmployeeService.GetEmployeeById(id);
            if (empViewVM == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewVM);
        }

        [HttpPost]
        [Route("{employeeId:int}/UpdatePay")]
        public HttpResponseMessage UpdatePay(HttpRequestMessage request, int employeeId, EmpPayUpdateRequest vm)
        {
            var empViewVM = m_EmployeeService.UpdateEmployeePayHist(employeeId, vm);
            if (empViewVM == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewVM);
        }

        [HttpPost]
        [Route("{employeeId:int}/UpdateDept")]
        public HttpResponseMessage UpdateDept(HttpRequestMessage request,  int employeeId, EmpDeptUpdateRequest vm)
        {
            var empViewVM = m_EmployeeService.UpdateEmployeeDepartment(employeeId, vm);
            if (empViewVM == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewVM);
        }
    }
}
