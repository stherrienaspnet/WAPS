using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WAPS.BookStore.WebUI.Security
{
    public class HTTPSGuard:DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.RequestUri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
            {
                HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.BadRequest, "HTTPS is required for security reason.");
                return Task.FromResult(reply);
            }
                
            return base.SendAsync(request, cancellationToken);
        }        
    }
}