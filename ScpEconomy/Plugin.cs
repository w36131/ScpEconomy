using PluginAPI.Enums;
using PluginAPI.Core.Attributes;
using PluginAPI.Helpers;
using System;
using System.IO;
using System.Reflection;
using PluginAPI.Events;
using PluginAPI.Core;

namespace ScpEconomy
{
    public class Plugin
    {
        public const string PluginVersion = "0.0.0";

        public static Plugin Instance { get; private set; }

        [PluginConfig]
        public Config Config;

        public string PlayerDataDirectory { get; private set; }

        [PluginEntryPoint("ScpEconomy", PluginVersion, "Plugin that adds economy to your SCP:SL server.", "w36131")]
        public void OnLoad()
        {
            if (!Config.IsEnabled)
                return;

            Instance = this;

            ServerConsole.AddLog($"ScpEconomy-{PluginVersion} has been loaded.\nBy w36131", ConsoleColor.Green);

            PlayerDataDirectory = Path.Combine(Path.GetDirectoryName(PluginHandler.Get(this).MainConfigPath), "PlayerData");

            if (!Directory.Exists(PlayerDataDirectory))
            {
                Directory.CreateDirectory(PlayerDataDirectory);
            }

            EventManager.RegisterAllEvents(this);
        }

        [PluginUnload]
        public void OnUnload()
        {
            Instance = null;

            EventManager.UnregisterAllEvents(this);
        }

        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerJoinedEvent ev)
        {

        }
    }
}
