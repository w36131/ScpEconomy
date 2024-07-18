using System.Collections.Generic;

namespace ScpEconomy.API.DataObjects
{
    public class PlayerData
    {
        public string UserId { get; set; }
        public int Credits { get; set; } = 0;
        public List<VirtualItem> Inventory { get; set; } = new();
    }
}
