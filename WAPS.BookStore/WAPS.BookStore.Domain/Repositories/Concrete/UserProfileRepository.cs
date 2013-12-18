using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MTTWebAPI.Domain.Entities;
using WAPS.BookStore.Domain.Extenstions;
using WAPS.BookStore.Domain.Factories;
using WAPS.BookStore.Domain.Repositories.Abstract;

namespace WAPS.BookStore.Domain.Repositories.Concrete
{
    class UserProfileRepository : IUserProfileRepository
    {
        public void SetSessionId(string username, Guid sessionId, DateTime sessionExpireAt)
        {
            var sqlCmd = @"UPDATE [dbo].[UserProfile]
                           SET [SessionId] = '@sessionId',
                              [SessionExpireAt] = '@sessionExpireAt'
                          WHERE Email = '@username'";

            sqlCmd = sqlCmd.Replace("@username", username)
                           .Replace("@sessionId", sessionId.ToString())
                           .Replace("@sessionExpireAt", sessionExpireAt.ToString("yyyy-MM-ddTHH:mm:ss"));

            using (SqlConnection connection = SqlConnectionFactory.Create())
            {
                connection.Execute(sqlCmd.ToSqlFormat());
            }
        }

        public void SetExpireAt(string username, DateTime sessionExpireAt)
        {
            var sqlCmd = @"UPDATE [dbo].[UserProfile]
                           SET [SessionExpireAt] = '@sessionExpireAt'
                           WHERE Email = '@username'";

            sqlCmd = sqlCmd.Replace("@username", username)
                           .Replace("@sessionExpireAt", sessionExpireAt.ToString("yyyy-MM-ddTHH:mm:ss"));

            using (SqlConnection connection = SqlConnectionFactory.Create())
            {
                connection.Execute(sqlCmd.ToSqlFormat());
            }
        }
        
        public bool IsSessionValid(string username, Guid sessionId)
        {
            var sqlCmd = @"SELECT COUNT(*)
                           FROM [dbo].[UserProfile]
                           WHERE Email = '@username'
                           AND SessionId = '@sessionId'
                           AND SessionExpireAt > GETUTCDATE()";

            sqlCmd = sqlCmd.Replace("@username", username)
                           .Replace("@sessionId", sessionId.ToString());

            using (SqlConnection connection = SqlConnectionFactory.Create())
            {
                return connection.Query<int>(sqlCmd.ToSqlFormat()).Single() > 0;
            }
        }
    }
}
