using AWHumanResources.Data;
using AWHumanResources.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AWHumanResources.Web.Controllers
{
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiController
    {
        private readonly DepartmentService m_DepartmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            m_DepartmentService = departmentService;
        }

        [HttpGet]
        public List<DepartmentDto> Get()
        {
            return m_DepartmentService.GetDepartments();
        }
    }
}