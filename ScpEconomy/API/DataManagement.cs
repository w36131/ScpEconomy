using PluginAPI.Core;
using ScpEconomy.API.DataObjects;
using System;
using System.IO;
using System.Linq;
using Utf8Json;

namespace ScpEconomy.API
{
    public class DataManagement
    {
        public static PlayerData GetPlayerData(string userId)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                return null;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText);

            return readData;
        }
        public static PlayerData GetPlayerData(Player player)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                return null;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText);

            return readData;
        }

        public static void AddCredits(string userId, int amount)
        {
            try
            {
                if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                    return;

                string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

                PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText); readData.Credits += amount;

                var newData = readData;

                using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(readData), 0, JsonSerializer.Serialize(readData).Length);

                    fileStream.Close();
                }
            }
            catch(Exception e)
            {
                ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown: {e}.", ConsoleColor.Red);
            }
        }
        public static void AddCredits(Player player, int amount)
        {
            try
            {
                if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                    return;

                string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

                PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText); readData.Credits += amount;

                var newData = readData;

                using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(readData), 0, JsonSerializer.Serialize(readData).Length);

                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown: {e}.", ConsoleColor.Red);
            }
        }

        public static void RemoveCredits(string userId, int amount)
        {
            try
            {
                if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                    return;

                string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

                PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText); readData.Credits -= amount;

                var newData = readData;

                using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(readData), 0, JsonSerializer.Serialize(readData).Length);

                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown: {e}.", ConsoleColor.Red);
            }
        }
        public static void RemoveCredits(Player player, int amount)
        {
            try
            {
                if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                    return;

                string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

                PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText); readData.Credits -= amount;

                var newData = readData;

                using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(readData), 0, JsonSerializer.Serialize(readData).Length);

                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown: {e}.", ConsoleColor.Red);
            }
        }

        public static void AddVirtualItem(string userId, string name)
        {
            try
            {
                if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                    return;

                string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

                PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText);

                var foundVirtualItem = VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == name);

                if (foundVirtualItem != null)
                {
                    if (foundVirtualItem is InventoryVirtualItem || foundVirtualItem is CollectableVirtualItem)
                        readData.Inventory.Add(foundVirtualItem);
                    else
                        return;
                }
                else
                    return;

                var newData = readData;

                using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(readData), 0, JsonSerializer.Serialize(readData).Length);

                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown: {e}.", ConsoleColor.Red);
            }
        }
        public static void AddVirtualItem(string userId, VirtualItem virtualItem)
        {
            try
            {
                if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                    return;

                string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

                PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText);

                if (virtualItem != null)
                {
                    if (virtualItem is InventoryVirtualItem || virtualItem is CollectableVirtualItem)
                        readData.Inventory.Add(virtualItem);
                    else
                        return;
                }
                else
                    return;

                var newData = readData;

                using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(readData), 0, JsonSerializer.Serialize(readData).Length);

                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown: {e}.", ConsoleColor.Red);
            }
        }
        public static void AddVirtualItem(Player player, string name)
        {
            try
            {
                if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                    return;

                string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

                PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText);

                var foundVirtualItem = VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == name);

                if (foundVirtualItem != null)
                {
                    if (foundVirtualItem is InventoryVirtualItem || foundVirtualItem is CollectableVirtualItem)
                        readData.Inventory.Add(foundVirtualItem);
                    else
                        return;
                }
                else
                    return;

                var newData = readData;

                using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(readData), 0, JsonSerializer.Serialize(readData).Length);

                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown: {e}.", ConsoleColor.Red);
            }
        }
        public static void AddVirtualItem(Player player, VirtualItem virtualItem)
        {
            try
            {
                if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                    return;

                string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

                PlayerData readData = JsonSerializer.Deserialize<PlayerData>(readText);

                if (virtualItem != null)
                {
                    if (virtualItem is InventoryVirtualItem || virtualItem is CollectableVirtualItem)
                        readData.Inventory.Add(virtualItem);
                    else
                        return;
                }
                else
                    return;

                var newData = readData;

                using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                {
                    fileStream.Write(JsonSerializer.Serialize(readData), 0, JsonSerializer.Serialize(readData).Length);

                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[ScpEconomy:ERROR] Exception has been thrown: {e}.", ConsoleColor.Red);
            }
        }

    }
}
