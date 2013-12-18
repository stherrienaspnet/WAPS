using System;

namespace WAPS.BookStore.Domain.Repositories.Abstract
{
    public interface IUserProfileRepository
    {
        void SetSessionId(string username, Guid sessionId, DateTime sessionExpireAt);
        void SetExpireAt(string username, DateTime sessionExpireAt);
        bool IsSessionValid(string username, Guid sessionId);
    }
}
