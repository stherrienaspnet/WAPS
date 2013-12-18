using System;
using System.Configuration;
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

        [HttpPost]
        public Status Login(User user)
        {
            if (user == null)
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Please provide the credentials.") });

            if (!_webSecurityService.Login(user.UserId, user.Password))
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("Invalid user name or password.")
                });

            var expirationDelay = int.Parse(ConfigurationManager.AppSettings["SessionExpiration"]);
            var sessionExpireAt = DateTime.UtcNow.AddMinutes(expirationDelay);

            var sessionId = Guid.NewGuid();
            _webSecurityService.SetUserSession(user.UserId, sessionId, sessionExpireAt);

            Token token = new Token(user.UserId, Request.GetClientIP(), sessionId.ToString());
            return new Status { Successeded = true, Token = token.Encrypt(), Message = "Successfully signed in." };
        }

        [HttpPost]
        public Status Logout(User user)
        {
            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("Please provide the credentials.")
                });
            }

            _webSecurityService.AbandonUserSession(user.UserId);

            return new Status { Successeded = true, Token = null, Message = "Successfully signed out." };
        }
    }

}
