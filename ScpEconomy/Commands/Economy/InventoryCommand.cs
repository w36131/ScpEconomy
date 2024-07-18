using CommandSystem;
using Microsoft.Win32;
using PluginAPI.Core;
using ScpEconomy.API.DataObjects;
using System;
using Mirror;
using ScpEconomy.API;
using System.Linq;
using RemoteAdmin;
using PluginAPI.Commands;
using ScpEconomy.API.DataManagement;

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

            var playerInventory = API.DataManagement.Inventory.GetInventory(playerSender);

            if(playerInventory == null)
            {
                response = "Error! Something went wrong. Sorry!";
                return false;
            }

            response = "\n\n Your inventory:\n";

            foreach (var virtualItem in playerInventory)
            {
                response += $"\n  <color={VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem.Name).VirtualItemColor.ToHex()}><b>{VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem.Name).Name}</b></color>\n  <size=20%><color={VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem.Name).VirtualItemColor.ToHex()}>{VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem.Name).Description}</color></size>\n";
            }

            return true;
        }
    }
}
