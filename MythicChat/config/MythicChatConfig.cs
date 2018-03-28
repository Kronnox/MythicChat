/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 28, 2018
* ------------------------------------
**/

using System.Collections.Generic;

namespace MythicChat.config
{
    public class MythicChatConfig
    {
        public Dictionary<string, object> configValues = new Dictionary<string, object>();

        public MythicChatConfig() {}

        public Dictionary<string, object> GetConfigValues()
        {
            return this.configValues;
        }

        public void SetConfigValues(Dictionary<string, object> input)
        {
            this.configValues = input;
        }
    }
}
