using System.Net.Http;
using MTTWebAPI.WebUI;
using SimpleInjector;
using WAPS.BookStore.Domain.Injection;
using WAPS.BookStore.WebUI.Security;

namespace WAPS.BookStore.WebUI.Injection
{
    public class Bootstrapper
    {
        public static void Initialise(Container container)
        {
            RegisterWebPackage(container);
            DomainInjectionRegistration.RegisterPackage(container);
        }

        private static void RegisterWebPackage(Container container)
        {
            container.Register<IWebApiConfig, WebApiConfig>();
            container.Register<DelegatingHandler, TokenInspector>();
        }
    }
}