using System;
using WAPS.BookStore.Domain.Repositories.Abstract;
using WAPS.BookStore.Domain.Services.Abstract;
using WebMatrix.WebData;

namespace WAPS.BookStore.Domain.Services.Concrete
{
    public class WebSecurityService : IWebSecurityService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public WebSecurityService(IFeatureRepository featureRepository, IUserProfileRepository userProfileRepository)
        {
            _featureRepository = featureRepository;
            _userProfileRepository = userProfileRepository;
        }

        public string EnableSimpleMembershipKey
        {
            get { return WebSecurity.EnableSimpleMembershipKey; }
        }

        public int CurrentUserId
        {
            get { return WebSecurity.CurrentUserId; }
        }

        public string CurrentUserName
        {
            get { return WebSecurity.CurrentUserName; }
        }

        public bool HasUserId
        {
            get { return WebSecurity.HasUserId; }
        }

        public bool Initialized
        {
            get { return WebSecurity.Initialized; }
        }

        public bool IsAuthenticated
        {
            get { return WebSecurity.IsAuthenticated; }
        }

        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(userName, currentPassword, newPassword);
        }

        public bool ConfirmAccount(string accountConfirmationToken)
        {
            return WebSecurity.ConfirmAccount(accountConfirmationToken);
        }

        public bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            return WebSecurity.ConfirmAccount(userName, accountConfirmationToken);
        }

        public string CreateAccount(string userName, string password, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateAccount(userName, password, requireConfirmationToken);
        }

        public string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateUserAndAccount(userName, password, propertyValues, requireConfirmationToken);
        }

        public string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow = 1440)
        {
            return WebSecurity.GeneratePasswordResetToken(userName, tokenExpirationInMinutesFromNow);
        }

        public DateTime GetCreateDate(string userName)
        {
            return WebSecurity.GetCreateDate(userName);
        }

        public DateTime GetLastPasswordFailureDate(string userName)
        {
            return WebSecurity.GetLastPasswordFailureDate(userName);
        }

        public DateTime GetPasswordChangedDate(string userName)
        {
            return WebSecurity.GetPasswordChangedDate(userName);
        }

        public int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            return WebSecurity.GetPasswordFailuresSinceLastSuccess(userName);
        }

        public int GetUserId(string userName)
        {
            return WebSecurity.GetUserId(userName);
        }

        public int GetUserIdFromPasswordResetToken(string token)
        {
            return WebSecurity.GetUserIdFromPasswordResetToken(token);
        }

        public void InitializeDatabaseConnection(string connectionStringName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        {
            WebSecurity.InitializeDatabaseConnection(connectionStringName, userTableName, userIdColumn, userNameColumn, autoCreateTables);
        }

        public void InitializeDatabaseConnection(string connectionString, string providerName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        {
            WebSecurity.InitializeDatabaseConnection(connectionString, providerName, userTableName, userIdColumn, userNameColumn, autoCreateTables);
        }

        public bool IsAccountLockedOut(string userName, int allowedPasswordAttempts, int intervalInSeconds)
        {
            return WebSecurity.IsAccountLockedOut(userName, allowedPasswordAttempts, intervalInSeconds);
        }

        public bool IsAccountLockedOut(string userName, int allowedPasswordAttempts, TimeSpan interval)
        {
            return WebSecurity.IsAccountLockedOut(userName, allowedPasswordAttempts, interval);
        }

        public bool IsConfirmed(string userName)
        {
            return WebSecurity.IsConfirmed(userName);
        }

        public bool IsCurrentUser(string userName)
        {
            return WebSecurity.IsCurrentUser(userName);
        }

        public bool Login(string userName, string password, bool persistCookie = false)
        {
            return WebSecurity.Login(userName, password, persistCookie);
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public void RequireAuthenticatedUser()
        {
            WebSecurity.RequireAuthenticatedUser();
        }

        public void RequireRoles(params string[] roles)
        {
            WebSecurity.RequireRoles(roles);
        }

        public void RequireUser(int userId)
        {
            WebSecurity.RequireUser(userId);
        }

        public void RequireUser(string userName)
        {
            WebSecurity.RequireUser(userName);
        }

        public bool ResetPassword(string passwordResetToken, string newPassword)
        {
            return WebSecurity.ResetPassword(passwordResetToken, newPassword);
        }

        public bool UserExists(string userName)
        {
            return WebSecurity.UserExists(userName);
        }

	    public bool CanUserAccessFeature(string username, string featureUrl)
	    {
		    return _featureRepository.CanUserAccessFeature(username, featureUrl);
	    }

        public void SetUserSession(string username, Guid sessionId, DateTime expireAt)
        {
            _userProfileRepository.SetSessionId(username, sessionId, expireAt);
        }

        public void SetUserExpiration(string username, DateTime expireAt)
        {
            _userProfileRepository.SetExpireAt(username, expireAt);
        }

        public bool IsSessionValid(string username, Guid sessionId)
        {
            return _userProfileRepository.IsSessionValid(username, sessionId);
        }

        public void AbandonUserSession(string username)
        {
            _userProfileRepository.SetExpireAt(username, DateTime.UtcNow);
        }
    }
}