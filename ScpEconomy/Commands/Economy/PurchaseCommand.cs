using CommandSystem;
using PluginAPI.Core;
using ScpEconomy.DataManagement;
using ScpEconomy.DataObjects;
using ScpEconomy.PurchaseActions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utf8Json;

namespace ScpEconomy.Commands.Economy
{
    public class PurchaseCommand : ICommand
    {
        public string Command => "Purchase";
        public string[] Aliases { get; } = { "Buy" };
        public string Description => "Purchases a given item.";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var playerSender = Player.Get(sender);

            if (playerSender.DoNotTrack)
            {
                response = "You have DoNotTrack (DNT) enabled, ScpEconomy cannot store you data while you have DNT enabled.";
                return false;
            }

            if(arguments.Count != 1)
            {
                response = "You didin't provide any arguments. Correct command usage: Purchase [Virtual item name]";
                return false;
            }

            var virtualItem = VirtualItem.Registered.FirstOrDefault(x => x.Name == arguments.At(0));

            if (virtualItem == null)
            {
                response = "The virtual item you provided doesn't exist!";
                return false;
            }

            Wallet.Get(playerSender, out int playerBalance);

            if(playerBalance < virtualItem.Price)
            {
                response = "You don't have enough balance to buy this virtual item!";
                return false;
            }

            foreach(var purchaseAction in virtualItem.PurchaseActions)
            {
                if (purchaseAction.GetType() == typeof(AddToInventoryPurchaseAction))
                {
                    Inventory.Add(playerSender, virtualItem.Name);
                }

                if (purchaseAction.GetType() == typeof(AssignBadgePurchaseAction))
                {
                    if (!File.Exists(Plugin.DataDirectory + $"\\Players\\{playerSender.UserId}.json"))
                        continue;

                    string readText = File.ReadAllText(Plugin.DataDirectory + $"\\Players\\{playerSender.UserId}.json");

                    var deserializedPlayerData = JsonSerializer.Deserialize<PlayerData>(readText); deserializedPlayerData.TemporaryBadge = new TemporaryBadge { GroupName = ((AssignBadgePurchaseAction)purchaseAction).GroupName, From = DateTime.UtcNow, To = DateTime.UtcNow + ((AssignBadgePurchaseAction)purchaseAction).TimeSpan };

                    using (FileStream fileStream = File.Create(Plugin.DataDirectory + $"\\Players\\{playerSender.UserId}.json"))
                    {
                        fileStream.Write(JsonSerializer.Serialize(deserializedPlayerData), 0, JsonSerializer.Serialize(deserializedPlayerData).Length);

                        fileStream.Close();
                    }
                    continue;
                }

                if (purchaseAction.GetType() == typeof(ExecuteCommandPurchaseAction))
                {
                    Server.RunCommand(((ExecuteCommandPurchaseAction)purchaseAction).Command);
                    continue;
                }
            }

            response = $"Purchase complete!";
            return true;
        }
    }
}
