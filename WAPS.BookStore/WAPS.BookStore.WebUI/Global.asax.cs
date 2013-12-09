using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MTTWebAPI.WebUI;
using SimpleInjector;
using WAPS.BookStore.WebUI.Injection;
using WebMatrix.WebData;

namespace WAPS.BookStore.WebUI
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
            // Create the container as usual.
            var container = new Container();

            var services = GlobalConfiguration.Configuration.Services;
            var controllerTypes = services.GetHttpControllerTypeResolver()
                .GetControllerTypes(services.GetAssembliesResolver());

            // register Web API controllers (important! http://bit.ly/1aMbBW0)
            foreach (var controllerType in controllerTypes)
            {
                container.Register(controllerType);
            }

            Bootstrapper.Initialise(container);

            // Verify the container configuration
            container.Verify();

            // Register the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

			AreaRegistration.RegisterAllAreas();
			IWebApiConfig webApiConfig = container.GetInstance<IWebApiConfig>();

			webApiConfig.Register(GlobalConfiguration.Configuration);
			ConfigureWebApi(GlobalConfiguration.Configuration);

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			ConfigureWebSecurity();
		}

		public void ConfigureWebSecurity()
		{
			if (!WebSecurity.Initialized)
			{
				WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "Email", autoCreateTables: true);
			}

			if (!Roles.RoleExists("Administrator"))
			{
				WebSecurity.CreateUserAndAccount("MTTWebAPIAdmin@gmail.com", "1999dpi!", new { FirstName = "System", LastName = "Administrator", Email = "MTTWebAPIAdmin@gmail.com", CreatedOn = DateTime.UtcNow });
				Roles.CreateRole("Administrator");
				Roles.AddUserToRole("MTTWebAPIAdmin@gmail.com", "Administrator");
			}
		}

		void ConfigureWebApi(HttpConfiguration config)
		{
			// Remove the XML formatter
			config.Formatters.Remove(config.Formatters.XmlFormatter);
		}
	}
}