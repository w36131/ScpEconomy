using PluginAPI.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScpEconomy.API.DataObjects
{
    public abstract class VirtualItem
    {
        public VirtualItem() => RegisteredVirtualItems.Add(this);

        public static HashSet<VirtualItem> RegisteredVirtualItems = new();

        public abstract string Name { get; set; }
        public virtual string Description { get; set; } = "No description provided.";
        public virtual int Price { get; set; } = 0;
        public virtual Color VirtualItemColor { get; set; } = Color.white;
    }
}
