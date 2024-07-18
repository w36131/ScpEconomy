using PluginAPI.Core;
using ScpEconomy.API.DataObjects;
using UnityEngine;

namespace ScpEconomy
{
    public class TestItem : InventoryVirtualItem
    {
        public override string Name { get; set; } = "TestItem";
        public override Color VirtualItemColor { get; set; } = Color.white;
    }
}
