using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace AWHumanResources.Web.Infrastructure.Filters
{
    public class ResourceNotFoundExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is DataException)
            {
                actionExecutedContext.Response = new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Resource not found.")
                };
            }
        }
    }
}