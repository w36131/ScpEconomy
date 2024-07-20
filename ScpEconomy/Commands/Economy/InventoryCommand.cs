using CommandSystem;
using PluginAPI.Core;
using ScpEconomy.DataManagement;
using System;

namespace ScpEconomy.Commands.Economy
{
    public class InventoryCommand : ICommand
    {
        public string Command => "Inventory";
        public string[] Aliases { get; } = { "Inv" };
        public string Description => "Shows your inventory.";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var playerSender = Player.Get(sender);

            if (playerSender.DoNotTrack)
            {
                response = "You have DoNotTrack (DNT) enabled, ScpEconomy cannot store you data while you have DNT enabled.";
                return false;
            }

            var playerInventory = Inventory.Get(playerSender);

            if(playerInventory == null)
            {
                response = "Something went wrong. Sorry!";
                return false;
            }

            response = "\n\n Your inventory:\n";

            foreach (var virtualItem in playerInventory)
            {
                response += $"\n  <color={virtualItem.Color.ToHex()}><b>{virtualItem.Name}</b></color>\n  <size=20%><color={virtualItem.Color.ToHex()}>{virtualItem.Description}</color></size>\n";
            }

            return true;
        }
    }
}
