using PluginAPI.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScpEconomy.API.DataObjects
{
    public abstract class VirtualItem
    {
        [NonSerialized()]
        public static List<VirtualItem> RegisteredVirtualItems = new();

        public abstract string Name { get; set; }
        public virtual string Description { get; set; } = "No description provided.";
        public virtual int Price { get; set; } = 0;
        public virtual Color ItemColor { get; set; } = Color.white;
        public virtual void Purchase(string userId)
        {
            if(DataManagement.GetPlayerData(userId) != null)
                if(DataManagement.GetPlayerData(userId).Credits >= Price)
                {
                    DataManagement.RemoveCredits(userId, Price);
                    OnPurchased(userId, Price);
                }
        }
        public virtual void Purchase(Player player)
        {
            if (DataManagement.GetPlayerData(player) != null)
                if (DataManagement.GetPlayerData(player).Credits >= Price)
                {
                    DataManagement.RemoveCredits(player, Price);
                    OnPurchased(player, Price);
                }
        }
        public abstract void OnPurchased(string userId, int virtualItemPrice);
        public abstract void OnPurchased(Player player, int virtualItemPrice);
    }
}
