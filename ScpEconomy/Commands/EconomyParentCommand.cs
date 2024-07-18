using CommandSystem;
using ScpEconomy.Commands.Economy;
using System;

namespace ScpEconomy.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class EconomyParentCommand : ParentCommand, ICommand
    {
        public EconomyParentCommand() => LoadGeneratedCommands();

        public override string Command => "Economy";
        public override string[] Aliases { get; } = { "Eco" };
        public override string Description => "Economy parent command.";
        public bool SanitizeResponse => false;

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new WalletCommand());
            RegisterCommand(new InventoryCommand());
            RegisterCommand(new ItemShopCommand());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "You didin't provide any sub-command.\n\n All available sub-commands:\n";

            foreach (var command in AllCommands)
            {
                response += $"\n  <b>{command.Command}</b>\n  <size=20%>{command.Description}</size>\n";
            }

            return false;
        }
    }
}
