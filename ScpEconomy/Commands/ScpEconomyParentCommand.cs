using CommandSystem;
using ScpEconomy.Commands.Administrative;
using ScpEconomy.Commands.Economy;
using System;

namespace ScpEconomy.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ScpEconomyParentCommand : ParentCommand, ICommand
    {
        public ScpEconomyParentCommand() => LoadGeneratedCommands();

        public override string Command => "ScpEconomy";
        public override string[] Aliases { get; } = { "ScpEco" };
        public override string Description => "ScpEconomy parent command.";
        public bool SanitizeResponse => false;

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new SetBalance());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "You didin't provide any sub-command.\n\n All available sub-commands:\n";

            foreach (var command in AllCommands)
            {
                response += $"\n  <b>{command.Command}</b>\n  <size=85%>{command.Description}</size>\n";
            }

            return false;
        }
    }
}
