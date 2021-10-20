using Application.Interfaces;
using Application.OpenApp;
using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewProject.Container
{
    public class StartUp
    {
        public static void Main()
        {
            var container = BuildContainer();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    scope.Resolve<MainWindow>();
            //}
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>();
            builder.RegisterType<ServiceClients>().As<InterfaceAppProduct>();
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();
            //builder.Register(context => new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap < MyModel MyDto >;
            //    //etc...
            //})).AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
