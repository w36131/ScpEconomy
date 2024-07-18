using ScpEconomy.API.DataObjects;
using System.IO;
using Utf8Json;

namespace ScpEconomy.API.DataManagement
{
    public class Wallet
    {
        public enum BalanceModificationType
        {
            Add = 0,
            Subtract = 1,
        }

        public static void GetBalance(PluginAPI.Core.Player player, out int playerBalance)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
            {
                playerBalance = -1;
                return;
            }

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            playerBalance = deserializedPlayerData.Balance;
            return;
        }
        public static void GetBalance(string userId, out int playerBalance)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
            {
                playerBalance = -1;
                return;
            }

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            playerBalance = deserializedPlayerData.Balance;
            return;
        }

        public static void SetBalance(PluginAPI.Core.Player player, int newBalance)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Balance = newBalance;

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void SetBalance(string userId, int newBalance)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.Balance = newBalance;

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }

        public static void ModifyBalance(PluginAPI.Core.Player player, BalanceModificationType modificationType, int amount)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            switch (modificationType)
            {
                case BalanceModificationType.Add:
                    deserializedPlayerData.Balance += amount;
                    break;
                case BalanceModificationType.Subtract:
                    deserializedPlayerData.Balance -= amount;
                    break;
            }

            using(FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{player.UserId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
        public static void ModifyBalance(string userId, BalanceModificationType modificationType, int amount)
        {
            if (!File.Exists(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
                return;

            string readText = File.ReadAllText(Plugin.PlayerDataDirectory + $"\\{userId}.json");

            var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText);

            switch (modificationType)
            {
                case BalanceModificationType.Add:
                    deserializedPlayerData.Balance += amount;
                    break;
                case BalanceModificationType.Subtract:
                    deserializedPlayerData.Balance -= amount;
                    break;
            }

            using (FileStream fileStream = File.Create(Plugin.PlayerDataDirectory + $"\\{userId}.json"))
            {
                fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                fileStream.Close();
            }
        }
    }
}
