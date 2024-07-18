using CommandSystem;
using System;

namespace ScpEconomy.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ScpEconomyParentCommand : ParentCommand
    {
        public override string Command => "ScpEconomy";
        public override string[] Aliases => new string[] { "economy", "eco" };
        public override string Description => "ScpEconomy parent command.";
        public bool SanatizeResponse => false;

        public override void LoadGeneratedCommands()
        {
            
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "<color=#ff0000>You didin't provide a subcommand.</color>\n\n<color=#009900>All avilable subcommands:</color>\n";

            foreach (var command in AllCommands)
            {
                response += $"<color=#00cc00>> {command.Command}</color>\n  <color=#ffffff>{command.Description}</color>\n\n";
            }

            return false;
        }
    }
}
