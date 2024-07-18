namespace ScpEconomy.API.DataObjects
{
    public abstract class InventoryVirtualItem : VirtualItem
    {
        public abstract void OnUsed();
    }
}
