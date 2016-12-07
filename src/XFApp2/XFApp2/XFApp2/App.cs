using System;
using Microsoft.Practices.Unity;
using Prism.Common;
using Prism.Events;
using Prism.Modularity;
using Prism.Navigation;
using Prism.Services;
using Prism.Unity;
using Prism.Unity.Extensions;
using Prism.Unity.Modularity;
using Xamarin.Forms;
using XFApp2.Views;

namespace XFApp2
{
    public class App : PrismApplication
    {
        private const string _navigationServiceName = "UnityPageNavigationService";

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void ConfigureContainer()
        {
            Container.AddNewExtension<DependencyServiceExtension>();

            Container.RegisterInstance(Logger);
            Container.RegisterInstance(ModuleCatalog);
            Container.RegisterType<IApplicationProvider, ApplicationProvider>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INavigationService, UnityCustomPageNavigationService>(_navigationServiceName);
            Container.RegisterType<IModuleManager, ModuleManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IModuleInitializer, UnityModuleInitializer>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDependencyService, Prism.Services.DependencyService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPageDialogService, PageDialogService>(new ContainerControlledLifetimeManager());
        }

        protected async override void OnInitialized()
        {
            await NavigationService.NavigateAsync("RootPage/Content2");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<Content1>();
            Container.RegisterTypeForNavigation<Content2>();
            Container.RegisterTypeForNavigation<Content3>();
            Container.RegisterTypeForNavigation<SubContent1>();
            switch (new Random().Next() % 3)
            {
                case 0:
                    Container.RegisterTypeForNavigation<MyTabbedMasterDetailPage>("RootPage");
                    break;
                case 1:
                    Container.RegisterTypeForNavigation<MyTabbedPage>("RootPage");
                    break;
                default:
                    Container.RegisterTypeForNavigation<MyCarouselPage>("RootPage");
                    break;
            }
        }

        public class MyTabbedMasterDetailPage : TabbedMasterDetailPage
        {
            public MyTabbedMasterDetailPage() : base()
            {
                this.Children.Add(new Content1());
                this.Children.Add(new Content2());
                this.Children.Add(new Content3());
            }
        }

        public class MyTabbedPage : TabbedPage
        {
            public MyTabbedPage() : base()
            {
                this.Children.Add(new Content1());
                this.Children.Add(new Content2());
                this.Children.Add(new Content3());
            }
        }

        public class MyCarouselPage : CarouselPage
        {
            public MyCarouselPage() : base()
            {
                this.Children.Add(new Content1());
                this.Children.Add(new Content2());
                this.Children.Add(new Content3());
            }
        }
    }
}