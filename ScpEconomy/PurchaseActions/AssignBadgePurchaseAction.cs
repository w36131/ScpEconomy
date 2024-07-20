using System;

namespace ScpEconomy.PurchaseActions
{
    public class AssignBadgePurchaseAction : PurchaseAction 
    { 
        public string GroupName { get; set; }

        public TimeSpan TimeSpan { get; set; } = TimeSpan.FromSeconds(1);
    }
}
