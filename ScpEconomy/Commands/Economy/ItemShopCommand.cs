using CommandSystem;
using PluginAPI.Core;
using ScpEconomy.API.DataObjects;
using ScpEconomy.API;
using System;
using System.Linq;

namespace ScpEconomy.Commands.Economy
{
    public class ItemShopCommand : ICommand
    {
        public string Command => "ItemShop";
        public string[] Aliases { get; } = { "Shop", "Store", "ItemStore" };
        public string Description => "Virtual item shop of this server.";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var playerSender = Player.Get(sender);

            response = "\n\n Item shop of this server:\n";

            foreach (var virtualItem in Plugin.Instance.Config.ItemShop)
            {
                response += $"\n  <color={VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem).VirtualItemColor.ToHex()}><b>{VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem).Name}</b></color>\n  <size=20%><color={VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem).VirtualItemColor.ToHex()}>{VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem).Description}</color></size>\n";
            }

            return true;
        }
    }
}
