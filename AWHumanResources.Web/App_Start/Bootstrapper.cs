using System.Diagnostics;
using System.Web.Http;

namespace AWHumanResources.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            Debug.WriteLine("Made it here");
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}
