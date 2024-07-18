using System.Collections.Generic;

namespace ScpEconomy
{
    public class Config
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public string Currency { get; set; } = "Coins";
        public List<string> ItemShop { get; set; } = new();
    }
}
