using AWHumanResources.Data;
using AWHumanResources.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AWHumanResources.Web.Controllers
{
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiController
    {
        private readonly DepartmentService m_DepartmentService;
        private readonly EmployeeService m_EmployeeService;

        public DepartmentController(DepartmentService departmentService, EmployeeService employeeService)
        {
            m_DepartmentService = departmentService;
            m_EmployeeService = employeeService;
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var deptDtos = m_DepartmentService.GetDepartments();
            if (deptDtos == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, deptDtos);
        }

        [HttpGet]
        [Route("{departmentId:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int departmentId)
        {
            var deptDto = m_DepartmentService.GetDepartmentById(departmentId);
            if (deptDto == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, deptDto);

        }

        [HttpGet]
        [Route("{departmentId:int}/employees")]
        public HttpResponseMessage GetByDepartment(HttpRequestMessage request, int departmentId)
        {
            var empViewDtos = m_EmployeeService.GetEmployeesByDepartmentId(departmentId);
            if (empViewDtos == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, empViewDtos);
        }
    }
}