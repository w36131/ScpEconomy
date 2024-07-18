using PluginAPI.Core;
using ScpEconomy.DataObjects;
using ScpEconomy.Enums;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utf8Json;

namespace ScpEconomy.DataManagement
{
    public class Inventory
    {
        public static List<VirtualItem> Get(Player player)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
                return null;

            var virtualItems = new List<VirtualItem>();

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            foreach (var virtualItem in deserializedPlayerData.Inventory)
            {
                if (!VirtualItem.Registered.Any(x => x.Name == virtualItem))
                    continue;

                virtualItems.Add(VirtualItem.Registered.FirstOrDefault(x => x.Name == virtualItem));
            }

            return virtualItems;
        }
        public static List<VirtualItem> Get(string userId)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
                return null;

            var virtualItems = new List<VirtualItem>();

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            foreach (var virtualItem in deserializedPlayerData.Inventory)
            {
                if (!VirtualItem.Registered.Any(x => x.Name == virtualItem))
                    continue;

                virtualItems.Add(VirtualItem.Registered.FirstOrDefault(x => x.Name == virtualItem));
            }

            return virtualItems;
        }

        public static void Add(Player player, string virtualItemName)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
                return;

            if (VirtualItem.Registered.Any(x => x.Name != virtualItemName || !x.Attributes.Contains(VirtualItemAttribute.InventoryVirtualItem)))
                return;

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Inventory.Add(virtualItemName);

            using (FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
            return;
        }
        public static void Add(string userId, string virtualItemName)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
                return;

            if (VirtualItem.Registered.Any(x => x.Name != virtualItemName || !x.Attributes.Contains(VirtualItemAttribute.InventoryVirtualItem)))
                return;

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Inventory.Add(virtualItemName);

            using (FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
            return;
        }

        public static void Remove(Player player, string virtualItemName)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            if (!deserializedPlayerData.Inventory.Contains(virtualItemName))
                return;

            deserializedPlayerData.Inventory.Remove(virtualItemName);

            using (FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
            return;
        }
        public static void Remove(string userId, string virtualItemName)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            if (!deserializedPlayerData.Inventory.Contains(virtualItemName))
                return;

            deserializedPlayerData.Inventory.Remove(virtualItemName);

            using (FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
            return;
        }
    }
}
