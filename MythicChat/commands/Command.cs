/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 28, 2018
* ------------------------------------
**/

using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using MythicChat.config;
using MythicChat.manager;
using MythicChat.util;
using System;
using System.Collections.Generic;

namespace MythicChat.commands
{
    public class Commands : IChatCommandHandler
    {
        //Plugin

        [ChatCommand("mythicchat", "MythicChat base command. Enter '/mythicchat help' for more", ChatAuthorizationLevel.User)]
        public static void BaseCommand(User user, string arg)
        {
            switch(arg)
            {
                case "help":
                    PrintHelp(user);
                    break;
                case "reloadconfig":
                    ReloadConfig(user);
                    break;
                default:
                    PrintHelp(user);
                    break;
            }
        }

        public static void PrintHelp(User user)
        {
            //makeshift solution... TODO: Something more fancy
            ChatManager.ServerMessageToPlayerAlreadyLocalized("<b><color=white>--=[ MythicChat Help ]=--</color></b>", user, false);
            ChatManager.ServerMessageToPlayerAlreadyLocalized("<color=white>/mythicchat help</color> - <color=#DCDCDC>Shows help-page for MythicChat</color>", user, false);
            ChatManager.ServerMessageToPlayerAlreadyLocalized("<color=white>/msg <user> <message></color> - <color=#DCDCDC>Sends a private message to another user</color>", user, false);
            ChatManager.ServerMessageToPlayerAlreadyLocalized("<color=white>/r <message></color> - <color=#DCDCDC>Answer to the last private message</color>", user, false);
            ChatManager.ServerMessageToPlayerAlreadyLocalized("<color=white>/l <message></color> - <color=#DCDCDC>Sends a message only users nearby you are able to read</color>", user, false);
            ChatManager.ServerMessageToPlayerAlreadyLocalized("<color=red>/staff <user> <message></color> - <color=#DCDCDC>Sends a message only other Admins and Devs are able to read</color>", user, false);
            ChatManager.ServerMessageToPlayerAlreadyLocalized("<color=red>/mythicchat reloadconfig</color> - <color=#DCDCDC>Realoads the MythicChat config-file</color>", user, false);

        }

        public static void ReloadConfig(User user)
        {
            user.Player.SendTemporaryMessageAlreadyLocalized("Reloading MythicChat config file...");
            ConfigManager.Instance.LoadConfigFile();
            user.Player.SendTemporaryMessageAlreadyLocalized("...Complete!");
        }

        //Local-Chat

        [ChatCommand("l", "Sends the given message only to players near you", ChatAuthorizationLevel.User)]
        public static void LocalChatCommand(User user, string msg)
        {
            float radius = ConfigManager.Instance.GetFloat("localchat-radius");
            List<User> usersInRange = UserUtil.GetUsersInRadius(user, radius);

            string msgMask = ConfigManager.Instance.GetString("localchat-mask");
            string msgOut = String.Format(msgMask, user.Name, msg);

            foreach (User rangeUser in usersInRange)
                ChatManager.ServerMessageToPlayerAlreadyLocalized(msgOut, rangeUser, false);
        }

        //Staff Chat

        [ChatCommand("staff", "Sends a message, only other admins and devs are able to read", ChatAuthorizationLevel.Admin)]
        public static void StaffChatCommand(User user, string msg)
        {
            List<User> staff = UserUtil.GetOnlineStaff();

            string msgMask = ConfigManager.Instance.GetString("staffchat-mask");
            string msgOut = String.Format(msgMask, user.Name, msg);

            foreach (User staffMember in staff)
                ChatManager.ServerMessageToPlayerAlreadyLocalized(msgOut, staffMember, false);
        }

        //Private Message

        [ChatCommand("msg", "Sends a private message, only you two are able to read", ChatAuthorizationLevel.User)]
        public static void MsgCommand(User user, User to, string msg)
        {
            SendPrivateMessage(user, to, msg);
        }

        [ChatCommand("r", "Sends a private message, to the last User who sent you a private message", ChatAuthorizationLevel.User)]
        public static void ReplyCommand(User user, string msg)
        {
            User to = ConversationManager.Instance.GetLastChatPartner(user);
            if (to == null)
            {
                user.Player.SendTemporaryErrorAlreadyLocalized("You don't have a previous ChatPartner!");
                return;
            }

            SendPrivateMessage(user, to, msg);
        }

        private static void SendPrivateMessage(User from, User to, string msg)
        {
            if(!UserUtil.GetOnlineUsers().Contains(to))
            {
                from.Player.SendTemporaryMessageAlreadyLocalized("The user '"+to.Name+"' isn't online at the moment!");
                return;
            }

            string msgToMask = ConfigManager.Instance.GetString("msg-to-mask");
            string msgToOut = String.Format(msgToMask, to.Name, msg);
            ChatManager.ServerMessageToPlayerAlreadyLocalized(msgToOut, from, false);

            string msgFromMask = ConfigManager.Instance.GetString("msg-from-mask");
            string msgFromOut = String.Format(msgFromMask, from.Name, msg);
            ChatManager.ServerMessageToPlayerAlreadyLocalized(msgFromOut, to, false);

            ConversationManager.Instance.SetLastChatPartner(to, from);
        }
    }
}
