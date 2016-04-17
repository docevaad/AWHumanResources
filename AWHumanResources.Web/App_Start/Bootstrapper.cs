using System.Diagnostics;
using System.Web.Http;

namespace AWHumanResources.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}
