using CommandSystem;
using PluginAPI.Core;
using ScpEconomy.DataManagement;
using System;
using System.IO;

namespace ScpEconomy.Commands.Administrative
{
    public class SetBalance : ICommand
    {
        public string Command => "SetBalance";
        public string[] Aliases { get; } = { "SetBal" };
        public string Description => "Sets the wallet balance of a given player.";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.PlayerSensitiveDataAccess))
            {
                response = "You don't have permission to execute this command.";
                return false;
            }
            if (arguments.Count == 0)
            {
                response = "You didin't provide any arguments. Correct command usage: SetBalance [Player name / Player Id / SteamId@steam] [New balance] or SetBalance [Your new balance]";
                return false;
            }

            var playerSender = Player.Get(sender);
            Player targetPlayer = null;

            if (arguments.Count == 1)
            {
                if(!int.TryParse(arguments.At(0), out int newSenderBalance))
                {
                    response = "Invalid argument! The [Your new balance] argument needs to be an integer.";
                    return false;
                }

                Wallet.Set(playerSender, newSenderBalance);
                response = $"Nice! Your balance was successfully set to {newSenderBalance}.";
                return true;
            }

            if (int.TryParse(arguments.At(0), out int playerId))
            {
                targetPlayer = Player.Get(playerId);
            }
            if (Player.Get(arguments.At(0)) != null)
            {
                targetPlayer = Player.Get(arguments.At(0));
            }
            if (!int.TryParse(arguments.At(1), out int newTargetBalance))
            {
                response = "Invalid argument! The [New Balance] argument needs to be an integer.";
                return false;
            }
            else
            {
                if (targetPlayer == null)
                {
                    if (File.Exists(Plugin.DataDirectory + $"\\Players\\{arguments.At(0)}.json"))
                    {
                        Wallet.Set(arguments.At(0), newTargetBalance);
                        response = $"Nice! Balance of {arguments.At(0)} was successfully set to {newTargetBalance}.";
                        return true;
                    }

                    response = $"Error! Given player was not found.";
                    return false;
                }
                else
                {
                    Wallet.Set(targetPlayer, newTargetBalance);
                    response = $"Nice! Balance of {targetPlayer.DisplayNickname} was successfully set to {newTargetBalance}.";
                    return true;
                }
            }
        }
    }
}
