using System.Collections.Generic;
using WAPS.BookStore.WebUI.Models;

namespace WAPS.BookStore.WebUI.Security
{
    public class IdentityStore
    {
        private static Dictionary<string, string> users = new Dictionary<string, string> { { "Alex", "@lex" }, { "Cruz", "cruz007" }, {"Sanjay", "pass"} };

        public static bool IsValidUser(User user)
        {
            if (!users.ContainsKey(user.UserId))
                return false;

            return users[user.UserId] == user.Password;
        }

        public static bool IsValidUserId(string userId)
        {
            return users.ContainsKey(userId);
        }

    }
}
