using ScpEconomy.API.DataObjects;
using System.Collections.Generic;
using System.Linq;

namespace ScpEconomy.API.DataManagement
{
    public class ItemShop
    {
        public static List<VirtualItem> GetAllVirtualItems()
        {
            var items = new List<VirtualItem>();

            if (Plugin.Instance.Config.ItemShop.Count == 0)
                return null;

            foreach(var virtualItem in Plugin.Instance.Config.ItemShop)
            {
                if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItem))
                    continue;

                items.Add(VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItem));
            }

            return items;
        }
        public static VirtualItem GetVirtualItem(string virtualItemName)
        {
            if (!VirtualItem.RegisteredVirtualItems.Any(x => x.Name == virtualItemName))
                return null;

            return VirtualItem.RegisteredVirtualItems.FirstOrDefault(x => x.Name == virtualItemName);
        }
    }
}
