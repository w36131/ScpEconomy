using ScpEconomy.DataObjects;
using System.IO;
using Utf8Json;

namespace ScpEconomy.DataManagement
{
    public class Wallet
    {
        public enum ModificationType
        {
            Add = 0,
            Subtract = 1,
        }

        public static void Get(PluginAPI.Core.Player player, out int playerBalance)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
            {
                playerBalance = -1;
                return;
            }

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            playerBalance = deserializedPlayerData.Balance;
            return;
        }
        public static void Get(string userId, out int playerBalance)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
            {
                playerBalance = -1;
                return;
            }

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            playerBalance = deserializedPlayerData.Balance;
            return;
        }

        public static void Set(PluginAPI.Core.Player player, int newBalance)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Balance = newBalance;

            using (FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void Set(string userId, int newBalance)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Balance = newBalance;

            using (FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }

        public static void Modify(PluginAPI.Core.Player player, ModificationType modificationType, int amount)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            switch (modificationType)
            {
                case ModificationType.Add:
                    deserializedPlayerData.Balance += amount;
                    break;
                case ModificationType.Subtract:
                    deserializedPlayerData.Balance -= amount;
                    break;
            }

            using(FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void Modify(string userId, ModificationType modificationType, int amount)
        {
            if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            switch (modificationType)
            {
                case ModificationType.Add:
                    deserializedPlayerData.Balance += amount;
                    break;
                case ModificationType.Subtract:
                    deserializedPlayerData.Balance -= amount;
                    break;
            }

            using (FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
    }
}
