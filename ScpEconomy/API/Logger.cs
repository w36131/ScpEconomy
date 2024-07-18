namespace ScpEconomy.API
{
    public class Logger
    {
        public static void AddInfo(string logMessage)
        {
            ServerConsole.AddLog("[ScpEconomy:INFO] " + logMessage, System.ConsoleColor.Cyan);
        }

        public static void AddWarn(string logMessage)
        {
            ServerConsole.AddLog("[ScpEconomy:WARN] " + logMessage, System.ConsoleColor.DarkYellow);
        }

        public static void AddError(string logMessage)
        {
            ServerConsole.AddLog("[ScpEconomy:ERROR] " + logMessage, System.ConsoleColor.Red);
        }
    }
}
