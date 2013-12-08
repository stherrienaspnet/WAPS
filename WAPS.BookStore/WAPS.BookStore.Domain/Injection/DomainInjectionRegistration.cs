using Microsoft.Practices.Unity;
using MTTWebAPI.Domain.Services.Concrete;
using WAPS.BookStore.Domain.Repositories.Abstract;
using WAPS.BookStore.Domain.Repositories.Concrete;
using WAPS.BookStore.Domain.Services.Abstract;

namespace WAPS.BookStore.Domain.Injection
{
	public class DomainInjectionRegistration
	{
		public static void RegisterPackage(IUnityContainer container)
		{
			// register all your components with the container here
			container.RegisterType<IMembershipService, MembershipService>();
			container.RegisterType<IWebSecurityService, WebSecurityService>();
			container.RegisterType<IFeatureRepository, FeatureRepository>();
		}
	}
}
