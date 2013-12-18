using System;

namespace WAPS.BookStore.Domain.Services.Abstract
{
    public interface IWebSecurityService
    {
        string EnableSimpleMembershipKey { get; }
        int CurrentUserId { get; }
        string CurrentUserName { get; }
        bool HasUserId { get; }
        bool Initialized { get; }
        bool IsAuthenticated { get; }
        bool ChangePassword(string userName, string currentPassword, string newPassword);
        bool ConfirmAccount(string accountConfirmationToken);
        bool ConfirmAccount(string userName, string accountConfirmationToken);
        string CreateAccount(string userName, string password, bool requireConfirmationToken = false);
        string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false);
        string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow = 1440);
        DateTime GetCreateDate(string userName);
        DateTime GetLastPasswordFailureDate(string userName);
        DateTime GetPasswordChangedDate(string userName);
        int GetPasswordFailuresSinceLastSuccess(string userName);
        int GetUserId(string userName);
        int GetUserIdFromPasswordResetToken(string token);
        void InitializeDatabaseConnection(string connectionStringName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables);
        void InitializeDatabaseConnection(string connectionString, string providerName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables);
        bool IsAccountLockedOut(string userName, int allowedPasswordAttempts, int intervalInSeconds);
        bool IsAccountLockedOut(string userName, int allowedPasswordAttempts, TimeSpan interval);
        bool IsConfirmed(string userName);
        bool IsCurrentUser(string userName);
        bool Login(string userName, string password, bool persistCookie = false);
        void Logout();
        void RequireAuthenticatedUser();
        void RequireRoles(params string[] roles);
        void RequireUser(int userId);
        void RequireUser(string userName);
        bool ResetPassword(string passwordResetToken, string newPassword);
        bool UserExists(string userName);
	    bool CanUserAccessFeature(string username, string featureUrl);
        void SetUserSession(string username, Guid sessionId, DateTime expireAt);
        bool IsSessionValid(string username, Guid sessionId);
        void AbandonUserSession(string username);
    }
}
