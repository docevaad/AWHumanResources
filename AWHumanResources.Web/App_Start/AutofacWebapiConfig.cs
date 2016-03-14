﻿using Autofac;
using Autofac.Integration.WebApi;
using AWHumanResources.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Tortuga.Chain;

namespace AWHumanResources.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            containerBuilder.RegisterType<SqlServerDataSource>()
                .WithParameter("connectionString", System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorksSqlServer"].ConnectionString)
                .SingleInstance();

            containerBuilder.RegisterType<DepartmentService>().InstancePerRequest();
            containerBuilder.RegisterType<EmployeeService>().InstancePerRequest();

            Container = containerBuilder.Build();

            return Container;
        }
    }
}
