using PluginAPI.Core;
using PluginAPI.Events;
using PluginAPI.Enums;
using PluginAPI.Core.Attributes;
using System;
using System.IO;
using Utf8Json;
using ScpEconomy.DataObjects;
using YamlDotNet.Serialization;
using System.Collections.Generic;
using System.Text;
using ScpEconomy.PurchaseActions;
using UnityEngine;
using Discord;
using System.Linq;
using ScpEconomy.DataManagement;

namespace ScpEconomy
{
    public class Plugin
    {
        public static Plugin Instance { get; private set; }

        [PluginConfig]
        public Config Config;

        public static string DataDirectory { get; private set; }

        [PluginEntryPoint("ScpEconomy", "0.0.0", "Plugin that adds economy to your SCP:SL server.", "w36131")]
        public void OnLoad()
        {
            if (!Config.IsEnabled)
                return;

            Instance = this;

            ServerConsole.AddLog($"ScpEconomy has been loaded.\nBy w36131", ConsoleColor.Green);

            DataDirectory = Path.Combine(Path.GetDirectoryName(PluginHandler.Get(this).MainConfigPath), "Data");

            try
            {
                if (!Directory.Exists(DataDirectory))
                    Directory.CreateDirectory(DataDirectory);

                if (!Directory.Exists(DataDirectory + "\\Players"))
                    Directory.CreateDirectory(DataDirectory + "\\Players");
            }
            catch (Exception ex) { ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown while creating directories: {ex}", ConsoleColor.Red); }

            if (!File.Exists(DataDirectory + "\\ItemShop.yml"))
            {
                try
                {
                    var yamlSerializer = new SerializerBuilder().Build();

                    var serializedString = yamlSerializer.Serialize(new List<VirtualItem>());

                    using (FileStream fileStraem = File.Create(DataDirectory + "\\ItemShop.yml"))
                    {
                        fileStraem.Write(Encoding.Default.GetBytes(serializedString), 0, Encoding.Default.GetBytes(serializedString).Length);

                        fileStraem.Close();
                    }
                }
                catch (Exception ex) { ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown while creating the item shop file: {ex}", ConsoleColor.Red); }
            }
            else
            {
                try
                {
                    var yamlDeserializer = new DeserializerBuilder().WithTagMapping("!AddToInventory", typeof(AddToInventoryPurchaseAction)).WithTagMapping("!AssignBadge", typeof(AssignBadgePurchaseAction)).WithTagMapping("!ExecuteCommand", typeof(ExecuteCommandPurchaseAction)).Build();

                    foreach (var virtualItem in yamlDeserializer.Deserialize<List<VirtualItem>>(File.ReadAllText(DataDirectory + "\\ItemShop.yml")))
                    {
                        VirtualItem.Registered.Add(virtualItem);
                    }
                }
                catch (Exception ex) { ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown while reading the item shop file: {ex}", ConsoleColor.Red); }
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
                    if (File.Exists(DataDirectory + $"\\Players\\{ev.Player.UserId}.json"))
                        File.Delete(DataDirectory + $"\\Players\\{ev.Player.UserId}.json");

                    return;
                }

                if (File.Exists(DataDirectory + $"\\Players\\{ev.Player.UserId}.json"))
                {
                    string readText = File.ReadAllText(DataDirectory + $"\\Players\\{ev.Player.UserId}.json");

                    var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

                    if (deserializedPlayerData.TemporaryBadge == null)
                        return;

                    if (DateTime.UtcNow > deserializedPlayerData.TemporaryBadge.To)
                    {
                        deserializedPlayerData.TemporaryBadge = null;
                    }
                    else
                    {
                        var serverGroups = ServerStatic.GetPermissionsHandler().GetAllGroups();

                        if (!serverGroups.ContainsKey(deserializedPlayerData.TemporaryBadge.GroupName))
                            return;

                        ev.Player.ReferenceHub.serverRoles.SetGroup(serverGroups.FirstOrDefault(x => x.Key == deserializedPlayerData.TemporaryBadge.GroupName).Value);
                    }
                    return;
                }

                var playerData = new PlayerData { UserId = ev.Player.UserId };

                using (FileStream fileStream = File.Create(DataDirectory + $"\\Players\\{ev.Player.UserId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(playerData), 0, JsonSerializer.Serialize(playerData).Length);

                    fileStream.Close();
                }
            }
            catch(Exception ex) { ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown while handling player joining: {ex}", ConsoleColor.Red); }
        }

        [PluginEvent(ServerEventType.PlayerDeath)]
        public void OnPlayerDeath(PlayerDeathEvent ev)
        {
            if (ev.Attacker.Team == ev.Player.Team)
                return;

            if (ev.Player.IsSCP)
            {
                Wallet.Modify(ev.Attacker, Wallet.ModificationType.Add, Config.BalanceAddedForKillingScps);

                if (!ev.Player.DoNotTrack)
                    ShowEarnedBalanceHint(ev.Player, Config.BalanceAddedForKillingScps);

                return;
            }

            if (ev.Player.IsHuman)
            {
                Wallet.Modify(ev.Attacker, Wallet.ModificationType.Add, Config.BalanceAddedForKillingEnemies);

                if (!ev.Player.DoNotTrack)
                    ShowEarnedBalanceHint(ev.Player, Config.BalanceAddedForKillingEnemies);

                return;
            }
        }

        [PluginEvent(ServerEventType.PlayerEscape)]
        public void OnPlayerEscape(PlayerEscapeEvent ev)
        {
            Wallet.Modify(ev.Player, Wallet.ModificationType.Add, Config.BalanceForEscaping);

            if (!ev.Player.DoNotTrack)
                ShowEarnedBalanceHint(ev.Player, Config.BalanceForEscaping);
        }

        public void ShowEarnedBalanceHint(Player player, int earnedAmount)
        {
            player.ReceiveHint(Config.EaringBalanceHint.Replace("{Amount}", earnedAmount.ToString()), Config.EaringBalanceHintDuration);
        }
    }
}
