/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 27, 2018
* ------------------------------------
**/

using Eco.Gameplay.Players;
using Eco.Shared.Math;
using System.Collections.Generic;
using System.Linq;

namespace MythicChat.util
{
    public static class UserUtil
    {
        public static List<User> GetUsersInRadius(User source, float radius)
        {
            List<User> users = new List<User>();

            foreach (User user in UserManager.OnlineUsers)
                if (Vector3.Distance(source.Player.Position, user.Player.Position) <= radius)
                    users.Add(user);

            return users;
        }

        public static List<User> GetOnlineUsers()
        {
            return UserManager.OnlineUsers.ToList();
        }

        public static List<User> GetOnlineStaff()
        {
            return (from user in GetOnlineUsers() where user.IsAdminOrDev select user).ToList();
        }

        public static List<string> GetUserNamesForUsers(List<User> users)
        {
            List<string> userNames = new List<string>();

            foreach (User user in users)
            {
                userNames.Add(user.Name);
            }

            return userNames;
        }
    }
}
