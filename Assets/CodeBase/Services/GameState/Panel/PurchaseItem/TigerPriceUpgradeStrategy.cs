namespace TigerClicker.CodeBase.Services.GameState
{
    public class TigerPriceUpgradeStrategy : IPriceUpgrade
    {
        public int CalculateNewPrice(int priceIncreaseCount)
        {
            return 300 * priceIncreaseCount;
        }
    }
}
