using System.Collections.Generic;
using TigerClicker.CodeBase.Data;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class PriceUpgrader
    {
        private readonly List<PurchaseItem> _purchaseItems;
        private readonly IPersistentData _persistentData;
        private readonly Dictionary<PurchaseItemType, IPriceUpgrade> _upgradeStrategies;

        public PriceUpgrader(PurchaseItemContent purchaseItemContent, IPersistentData persistentData)
        {
            _persistentData = persistentData;
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
            var purchases = _persistentData.PlayerData.PurchaseItems;
            foreach (PurchaseItem item in _purchaseItems)
            {
                int savedCount = purchases.TryGetValue(item.PurchaseItemType, out int count) ? count : 0;
                item.UpdatePriceIncreaseCount(savedCount);
                item.UpdatePrice(_upgradeStrategies[item.PurchaseItemType].CalculateNewPrice(savedCount));
            }
        }
    }
}
