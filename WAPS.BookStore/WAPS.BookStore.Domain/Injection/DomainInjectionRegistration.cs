using MTTWebAPI.Domain.Services.Concrete;
using SimpleInjector;
using WAPS.BookStore.Domain.Repositories.Abstract;
using WAPS.BookStore.Domain.Repositories.Concrete;
using WAPS.BookStore.Domain.Services.Abstract;
using WAPS.BookStore.Domain.Services.Concrete;

namespace WAPS.BookStore.Domain.Injection
{
	public class DomainInjectionRegistration
	{
        public static void RegisterPackage(Container container)
		{
			// register all your components with the container here
            container.Register<IMembershipService, MembershipService>();
            container.Register<IWebSecurityService, WebSecurityService>();
            container.Register<IFeatureRepository, FeatureRepository>();
            container.Register<IUserProfileRepository, UserProfileRepository>();
		}
	}
}
