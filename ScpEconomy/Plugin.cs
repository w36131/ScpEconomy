using PluginAPI.Enums;
using PluginAPI.Core.Attributes;
using System;
using System.IO;
using PluginAPI.Events;
using PluginAPI.Core;
using ScpEconomy.API.DataObjects;
using Utf8Json;
using System.Reflection;
using ScpEconomy.API;

namespace ScpEconomy
{
    public class Plugin
    {
        public const string PluginVersion = "0.0.0";

        public static Plugin Instance { get; private set; }

        [PluginConfig]
        public Config Config;

        public static string PlayerDataDirectory { get; private set; }

        [PluginEntryPoint("ScpEconomy", PluginVersion, "Plugin that adds economy to your SCP:SL server.", "w36131")]
        public void OnLoad()
        {
            if (!Config.IsEnabled)
                return;

            Instance = this;

            ServerConsole.AddLog($"ScpEconomy-{PluginVersion} has been loaded.\nBy w36131", ConsoleColor.Green);

            PlayerDataDirectory = Path.Combine(Path.GetDirectoryName(PluginHandler.Get(this).MainConfigPath), "PlayerData");

            try
            {
                if (!Directory.Exists(PlayerDataDirectory))
                    Directory.CreateDirectory(PlayerDataDirectory);
            }
            catch (Exception ex)
            {
                Logger.AddError($"Exception has been thrown: {ex}.");
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
            try
            {
                if (ev.Player.DoNotTrack == true)
                {
                    if (File.Exists(PlayerDataDirectory + $"\\{ev.Player.UserId}.json"))
                        File.Delete(PlayerDataDirectory + $"\\{ev.Player.UserId}.json");

                    return;
                }

                if (File.Exists(PlayerDataDirectory + $"\\{ev.Player.UserId}.json"))
                    return;

                var playerData = new PlayerData
                {
                    UserId = ev.Player.UserId,
                };

                using (FileStream fileStream = File.Create(PlayerDataDirectory + $"\\{ev.Player.UserId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(playerData), 0, JsonSerializer.Serialize(playerData).Length);

                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError($"[ScpEconomy:ERROR] Exception has been thrown: {ex}.");
            }
        }
    }
}
