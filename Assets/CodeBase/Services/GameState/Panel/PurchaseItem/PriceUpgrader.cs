using System.Collections.Generic;
using TigerClicker.CodeBase.Services.Visitors;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class PriceUpgrader
    {
        private readonly List<PurchaseItem> _purchaseItems;
        private readonly PurchaseItemCheker _purchaseItemCheker;
        private readonly Dictionary<PurchaseItemType, IPriceUpgrade> _upgradeStrategies;

        public PriceUpgrader(PurchaseItemContent purchaseItemContent, PurchaseItemCheker purchaseItemCheker)
        {
            _purchaseItemCheker = purchaseItemCheker;
            _purchaseItems = new List<PurchaseItem>(purchaseItemContent.PurchaseItems);

            _upgradeStrategies = new Dictionary<PurchaseItemType, IPriceUpgrade>
            {
                { PurchaseItemType.Tiger, new TigerPriceUpgradeStrategy() },
                { PurchaseItemType.Buildings, new ButcheryPriceUpgradeStrategy() }
            };

        }

        public void Initialize() => ApplyStartingPrices();

        public void Upgrade(PurchaseItemType type)
        {
            PurchaseItem item = _purchaseItems.Find(x => x.PurchaseItemType == type);
            item.UpdatePriceIncreaseCount(item.PriceIncreaseCount + 1);
            item.UpdatePrice(_upgradeStrategies[type].CalculateNewPrice(item.PriceIncreaseCount));
        }

        private void ApplyStartingPrices()
        {
            foreach (PurchaseItem item in _purchaseItems)
            {
                _purchaseItemCheker.Visit(item);
                item.UpdatePriceIncreaseCount(_purchaseItemCheker.PriceIncreaseCount);
                item.UpdatePrice(_upgradeStrategies[item.PurchaseItemType].CalculateNewPrice(item.PriceIncreaseCount));
            }
        }
    }
}
