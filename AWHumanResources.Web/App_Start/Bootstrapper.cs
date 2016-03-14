using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
