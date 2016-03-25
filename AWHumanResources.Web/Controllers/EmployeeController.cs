using AWHumanResources.Data.ViewModels;
using AWHumanResources.Services;
using AWHumanResources.Web.Infrastructure.Filters;
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
        [Route("{id:int}/UpdatePay")]
        public HttpResponseMessage UpdatePay(HttpRequestMessage request, EmpPayUpdateVM vm)
        {
            var empViewVM = m_EmployeeService.UpdateEmployeePayHist(vm);
            if (empViewVM == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewVM);
        }

        [HttpPost]
        [Route("{id:int}/UpdateDept")]
        public HttpResponseMessage UpdateDept(HttpRequestMessage request,  EmpDeptUpdateVM vm)
        {
            var empViewVM = m_EmployeeService.UpdateEmployeeDepartment(vm);
            if (empViewVM == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewVM);
        }
    }
}
