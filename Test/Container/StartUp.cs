using Application.Interfaces;
using Application.Mappers;
using Application.Model;
using Application.OpenApp;
using Autofac;
using AutoMapper;
using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using infrastructure.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewProjectNew.Service;

namespace ViewProject.Container
{
    public class StartUp
    {
        //public static void Construct()
        //{
        //    var container = BuildContainer();

        //    using (var scope = container.BeginLifetimeScope())
        //    {
        //        scope.Resolve<ClienteService>();
        //    }
        //}

        //public static Autofac.IContainer BuildContainer()
        //{
        //    var builder = new ContainerBuilder();
        //    builder.RegisterType<ClienteService>();
        //    builder.RegisterType<ServiceClients>().As<InterfaceAppProduct>();
        //    //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();
        //    //builder.Register(context => new MapperConfiguration(cfg =>
        //    //{
        //    //    cfg.CreateMap < MyModel MyDto >;
        //    //    //etc...
        //    //})).AsSelf().SingleInstance();

        //    return builder.Build();
        //}
        public Autofac.IContainer container { get; set; }
        public void OnStartup()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<Cleaner>().As<ICleaner>();
            //builder.RegisterType<Repository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MainWindow>();
            builder.RegisterType<ClienteService>();
            builder.RegisterType<ServiceClients>().As<InterfaceAppProduct>();

            builder.RegisterType<RepositoryProduct>().As<IProduct>();

            //builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps(assembliesToScan))).SingleInstance();
            //builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();

            RegisterMaps(builder);
            // Add the MainWindowclass and later resolve
            //build.RegisterType<MainWindow>().AsSelf();

            container = builder.Build();

        }

        private static void RegisterMaps(ContainerBuilder builder)
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var assembliesTypes = assemblyNames
                .Where(a => a.Name.Equals("Com.Davidsekar.Models", StringComparison.OrdinalIgnoreCase))
                .SelectMany(an => Assembly.Load(an).GetTypes())
                .Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
                .Distinct();

            var autoMapperProfiles = assembliesTypes
                .Select(p => (Profile)Activator.CreateInstance(p)).ToList();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappers());


                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
            //CreateMap<Client, ClientModel>().ReverseMap();
            //CreateMap<ClientModel, Client>().ReverseMap();
        }
    }
}
