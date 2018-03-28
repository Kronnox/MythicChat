/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 28, 2018
* ------------------------------------
**/

using MythicChat.util;

namespace MythicChat.config
{
    class ConfigManager
    {
        private static ConfigManager instance;

        private readonly string filePath = "Mods/MythicChat/";
        private readonly string fileName = "config.json";
        private MythicChatConfig config;

        //Instance

        public static ConfigManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ConfigManager();
                return instance;
            }
        }

        public ConfigManager()
        {
            LoadConfigFile();
        }

        //File IO

        public void LoadConfigFile()
        {
            config = ClassSerializer<MythicChatConfig>.Deserialize(filePath, fileName);
        }

        //Config Actions

        public object GetObject(string key)
        {
            object val;
            config.GetConfigValues().TryGetValue(key, out val);
            return val;
        }

        public string GetString(string key)
        {
            return this.GetObject(key).ToString();
        }

        public float GetFloat(string key)
        {
            return float.Parse(this.GetString(key));
        }
    }
}
