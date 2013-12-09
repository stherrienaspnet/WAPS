using System.Net;
using System.Net.Http;
using System.Web.Http;
using WAPS.BookStore.Domain.Services.Abstract;
using WAPS.BookStore.WebUI.Helpers;
using WAPS.BookStore.WebUI.Models;
using WAPS.BookStore.WebUI.Security;

namespace WAPS.BookStore.WebUI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IWebSecurityService _webSecurityService;

        public UserController(IWebSecurityService webSecurityService)
        {
            _webSecurityService = webSecurityService;
        }

        public Status Authenticate(User user)
        {
            if (user == null)
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Please provide the credentials.") });

            if (_webSecurityService.Login(user.UserId, user.Password))
            {
                Token token = new Token(user.UserId, Request.GetClientIP());
                return new Status { Successeded = true, Token = token.Encrypt(), Message = "Successfully signed in." };
            }
            throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Invalid user name or password.") });
        }
    }

}
