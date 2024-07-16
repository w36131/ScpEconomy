using PluginAPI.Core.Attributes;

namespace ScpEconomy
{
    public class Plugin
    {
        public const string PluginVersion = "0.0.0";

        public static Plugin Instance { get; private set; }

        [PluginConfig]
        public Config Config;

        [PluginEntryPoint("ScpEconomy", PluginVersion, "Plugin that adds economy to your SCP:SL server.", "w36131")]
        public void OnLoad()
        {
            Instance = this;
        }

        public void OnUnload()
        {
            Instance = null;
        }
    }
}
