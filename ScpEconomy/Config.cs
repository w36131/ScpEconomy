namespace ScpEconomy
{
    public class Config
    {
        public bool IsEnabled { get; set; } = true;

        public int BalanceAddedForKillingEnemies = 100;
        public int BalanceAddedForKillingScps = 500;
        public int BalanceForEscaping = 300;
        public string EaringBalanceHint = "You just have earned {Amount} balance!";
        public float EaringBalanceHintDuration = 5f;
    }
}
