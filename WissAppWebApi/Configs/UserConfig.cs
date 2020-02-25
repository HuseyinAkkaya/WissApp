using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WissAppWebApi.Configs
{
    public static class UserConfig
    {
        private static List<string> _loggedOutUsers = new List<string>();
        private static int _loggedOutUsersMaximumCount = 100;
        private static int _loggedOutUsersRemoveCount = 10;


        private static void CheckLoggedUsersCount()
        {

            if (_loggedOutUsers.Count > _loggedOutUsersMaximumCount)
                _loggedOutUsers.RemoveRange(0, _loggedOutUsersRemoveCount);


        }

        public static void AddLoggedOutUser(string userName)
        {
            CheckLoggedUsersCount();
            if (!_loggedOutUsers.Contains(userName))
                _loggedOutUsers.Add(userName);

        }

        public static void RemoveLoggedOutUser(string userName)
        {
            if (_loggedOutUsers.Contains(userName))
                _loggedOutUsers.Remove(userName);
        }

        public static List<string> GetLoggedOutUser()
        {
            return _loggedOutUsers;
        }
    }
}