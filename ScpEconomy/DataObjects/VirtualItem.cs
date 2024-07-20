using ScpEconomy.PurchaseActions;
using System.Collections.Generic;
using UnityEngine;

namespace ScpEconomy.DataObjects
{
    public class VirtualItem
    {
        public static HashSet<VirtualItem> Registered = new();

        public string Name { get; set; }
        public string Description { get; set; } = "No description provided.";
        public Color32 Color { get; set; } = new Color32(255, 255, 255, 255);
        public int Price { get; set; } = 0;
        public List<PurchaseAction> PurchaseActions { get; set; } = new List<PurchaseAction>();
    }
}
