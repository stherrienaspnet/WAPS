using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.Practices.Unity;
using MTTWebAPI.WebUI;
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
			Bootstrapper.Initialise();

			AreaRegistration.RegisterAllAreas();
			IWebApiConfig webApiConfig = Bootstrapper.Container.Resolve<IWebApiConfig>();

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