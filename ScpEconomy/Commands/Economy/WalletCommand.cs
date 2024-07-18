using CommandSystem;
using PluginAPI.Core;
using System;

namespace ScpEconomy.Commands.Economy
{
    public class WalletCommand : ICommand
    {
        public string Command => "Wallet";
        public string[] Aliases { get; } = { "Balance", "Bal" };
        public string Description => "Shows your balance.";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var playerSender = Player.Get(sender);

            if (playerSender.DoNotTrack)
            {
                response = "You have DoNotTrack (DNT) enabled, ScpEconomy cannot store you data while you have DNT enabled.";
                return false;
            }

            API.DataManagement.Wallet.GetBalance(playerSender, out int playerBalance);

            if(playerBalance == -1)
            {
                response = "Error! Something went wrong. Sorry!";
                return false;
            }

            response = $"\n\n <b>Your balance: {playerBalance} {Plugin.Instance.Config.Currency}</b>";
            return true;
        }
    }
}
