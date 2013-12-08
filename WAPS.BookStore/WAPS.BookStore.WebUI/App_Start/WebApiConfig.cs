using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace MTTWebAPI.WebUI
{
	public class WebApiConfig : IWebApiConfig
	{
		private readonly DelegatingHandler _tokenInspector;

		public WebApiConfig(DelegatingHandler tokenInspector)
	    {
		    _tokenInspector = tokenInspector;
	    }

	    public void Register(HttpConfiguration config)
	    {
            _tokenInspector.InnerHandler = new HttpControllerDispatcher(config);

		    config.Routes.MapHttpRoute(
                name: "Authentication",
                routeTemplate: "api/user/{id}",
                defaults: new { controller = "user" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: _tokenInspector
            );

            //config.MessageHandlers.Add(new HTTPSGuard()); //Global handler - applicable to all the requests
        }
    }
}
