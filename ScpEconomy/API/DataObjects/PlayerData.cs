using System.Collections.Generic;

namespace ScpEconomy.API.DataObjects
{
    public class PlayerData
    {
        public string UserId { get; set; }
        public int Balance { get; set; } = 0;
        public List<string> Inventory { get; set; } = new();
    }
}
