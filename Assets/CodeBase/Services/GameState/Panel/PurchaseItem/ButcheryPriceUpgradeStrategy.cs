namespace TigerClicker.CodeBase.Services.GameState
{
    public class ButcheryPriceUpgradeStrategy : IPriceUpgrade
    {
           public int CalculateNewPrice(int priceIncreaseCount)
        {
            return 150 * priceIncreaseCount;
        }

    }
}
