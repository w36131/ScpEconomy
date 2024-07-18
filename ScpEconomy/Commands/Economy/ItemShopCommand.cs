using CommandSystem;
using PluginAPI.Core;
using ScpEconomy.DataManagement;
using System;

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

            foreach (var virtualItem in ItemShop.Get())
            {
                response += $"\n  <color={virtualItem.Color.ToHex()}><b>{virtualItem.Name}</b> <size=20%>Price: <i>{(virtualItem.Price <= 0 ? "Free" : virtualItem.Price)}</i></size></color>\n  <size=20%><color={virtualItem.Color.ToHex()}>{virtualItem.Description}</color></size>\n";
            }

            return true;
        }
    }
}
