using PluginAPI.Core;
using ScpEconomy.API.DataObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utf8Json;

namespace ScpEconomy.API.DataManagement
{
    public class Inventory
    {
        public static List<VirtualItem> GetInventory(Player player)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                return null;

            var inventory = new List<VirtualItem>();

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            foreach(var virtualItem in deserializedPlayerData.Inventory)
            {
                if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItem))
                {
                    deserializedPlayerData.Inventory.Remove(virtualItem);
                    continue;
                }

                inventory.Add(VirtualItem.RegisteredVirtualItems.First(x => x.Name == virtualItem));
            }

            if (inventory.Count == 0)
                inventory = null;

            return inventory;
        }
        public static List<VirtualItem> GetInventory(string userId)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                return null;

            var inventory = new List<VirtualItem>();

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            foreach (var virtualItem in deserializedPlayerData.Inventory)
            {
                if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItem))
                {
                    deserializedPlayerData.Inventory.Remove(virtualItem);
                    continue;
                }

                inventory.Add(VirtualItem.RegisteredVirtualItems.First(x => x.Name == virtualItem));
            }

            if (inventory.Count == 0)
                inventory = null;

            return inventory;
        }

        public static void AddVirtualItem(Player player, string virtualItemName)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                return;

            if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItemName))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Inventory.Add(virtualItemName);

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void AddVirtualItem(string userId, string virtualItemName)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                return;

            if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItemName))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Inventory.Add(virtualItemName);

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }

        public static void RemoveVirtualItem(Player player, string virtualItemName)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                return;

            if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItemName))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            if (deserializedPlayerData.Inventory.Any(x => x == virtualItemName))
                deserializedPlayerData.Inventory.Remove(virtualItemName);
            else
                return;

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void RemoveVirtualItem(Player player, VirtualItem virtualItem)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                return;

            if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItem.Name))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            if (deserializedPlayerData.Inventory.Any(x => x == virtualItem.Name))
                deserializedPlayerData.Inventory.Remove(virtualItem.Name);
            else
                return;

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void RemoveVirtualItem(string userId, string virtualItemName)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                return;

            if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItemName))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            if (deserializedPlayerData.Inventory.Any(x => x == virtualItemName))
                deserializedPlayerData.Inventory.Remove(virtualItemName);
            else
                return;

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void RemoveVirtualItem(string userId, VirtualItem virtualItem)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                return;

            if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItem.Name))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            if (deserializedPlayerData.Inventory.Any(x => x == virtualItem.Name))
                deserializedPlayerData.Inventory.Remove(virtualItem.Name);
            else
                return;

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }

        public static void ClearInventory(Player player)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Inventory.Clear();

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void ClearInventory(string userId)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Inventory.Clear();

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
    }
}
