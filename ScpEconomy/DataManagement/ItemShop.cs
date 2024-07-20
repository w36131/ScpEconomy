using ScpEconomy.DataObjects;
using ScpEconomy.PurchaseActions;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace ScpEconomy.DataManagement
{
    public class ItemShop
    {
        public static List<VirtualItem> Get()
        {
            var virtualItems = new List<VirtualItem>();

            if (File.Exists(Plugin.DataDirectory + "\\ItemShop.yml"))
            {
                var yamlDeserializer = new DeserializerBuilder().WithTagMapping("!AddToInventory", typeof(AddToInventoryPurchaseAction)).WithTagMapping("!AssignBadge", typeof(AssignBadgePurchaseAction)).WithTagMapping("!ExecuteCommand", typeof(ExecuteCommandPurchaseAction)).Build();

                foreach (var virtualItem in yamlDeserializer.Deserialize<List<VirtualItem>>(File.ReadAllText(Plugin.DataDirectory + "\\ItemShop.yml")))
                {
                    virtualItems.Add(virtualItem);
                }

                return virtualItems;
            }

            return null;
        }
    }
}
