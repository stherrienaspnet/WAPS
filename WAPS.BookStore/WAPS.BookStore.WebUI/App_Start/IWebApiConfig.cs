using System.Web.Http;

namespace MTTWebAPI.WebUI
{
	public interface IWebApiConfig
	{
		void Register(HttpConfiguration config);
	}
}