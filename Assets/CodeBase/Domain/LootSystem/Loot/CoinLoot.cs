using TigerClicker.CodeBase.Domain;

namespace TigerClicker.CodeBase.Domain.LootSystem
{
    public class CoinLoot : ILoot
    {
        private const int coinAmount = 5;
        public int Amount => coinAmount;
        public void GenerateLoot(Wallet wallet)
        {
            wallet.AddCurrency(coinAmount, CurrencyType.Coin);
        }
    }
}
