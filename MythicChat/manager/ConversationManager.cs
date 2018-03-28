/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 28, 2018
* ------------------------------------
**/

using Eco.Gameplay.Players;
using System.Collections.Generic;

namespace MythicChat.manager
{
    class ConversationManager
    {
        private static ConversationManager instance;

        private Dictionary<string, User> lastChatPartners = new Dictionary<string, User>();

        //Instance

        public static ConversationManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ConversationManager();
                return instance;
            }
        }

        //Main

        public void SetLastChatPartner(User user, User partner)
        {
            if (lastChatPartners.ContainsKey(user.Name))
                lastChatPartners.Remove(user.Name);

            lastChatPartners.Add(user.Name, partner);
        }

        public User GetLastChatPartner(User user)
        {
            if (!lastChatPartners.ContainsKey(user.Name))
                return null;

            return lastChatPartners[user.Name];
        }
    }
}
