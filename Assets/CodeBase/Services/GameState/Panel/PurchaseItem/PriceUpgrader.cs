using System.Collections.Generic;
using UnityEngine;
using TigerClicker.CodeBase.Services.Visitors;


namespace TigerClicker.CodeBase.Services.GameState
{
    public class PriceUpgrader
    {
        private List<PurchaseItem> _purchaseItems;
        private PurchaseItemCheker _purchaseItemCheker;
        private Dictionary<PurchaseItemType, IPriceUpgrade> _upgradeStrategies;
        public PriceUpgrader(IEnumerable<PurchaseItem> purchaseItems, PurchaseItemCheker purchaseItemCheker)
        {
            _purchaseItems = new List<PurchaseItem>(purchaseItems);
            _purchaseItemCheker = purchaseItemCheker;

            InitializePriceUpgrade();
            SetStartingHeroStats();
        }
        private PurchaseItem FindPurchaseItemByType(PurchaseItemType purchaseItemType)
        {
            return _purchaseItems.Find(x => x.PurchaseItemType == purchaseItemType);
        }

        private void InitializePriceUpgrade()
        {
            _upgradeStrategies = new Dictionary<PurchaseItemType, IPriceUpgrade>
    {
        { PurchaseItemType.Tiger, new TigerPriceUpgradeStrategy() },
        { PurchaseItemType.Buildings, new ButcheryPriceUpgradeStrategy() }
    };
        }

        private void SetStartingHeroStats()
        {
            foreach (PurchaseItem purchaseItem in _purchaseItems)
            {
                _purchaseItemCheker.Visit(purchaseItem);
                purchaseItem.UpdatePriceIncreaseCount(_purchaseItemCheker.PriceIncreaseCount);
                int newPrice = _upgradeStrategies[purchaseItem.PurchaseItemType].CalculateNewPrice(purchaseItem.PriceIncreaseCount);
                purchaseItem.UpdatePrice(newPrice);
            }
        }
        private void UpgradePrice(PurchaseItemType purchaseItemType)
        {
            PurchaseItem item = FindPurchaseItemByType(purchaseItemType);
            IPriceUpgrade strategy = _upgradeStrategies[purchaseItemType];

            item.UpdatePriceIncreaseCount(item.PriceIncreaseCount + 1);
            int newPrice = strategy.CalculateNewPrice(item.PriceIncreaseCount);

            item.UpdatePrice(newPrice);
        }

        public void UpgradePriceForTiger(PurchaseItemType purchaseItemType)
        {
            UpgradePrice(purchaseItemType);

        }
        public void UpgradePriceForButchery(PurchaseItemType purchaseItemType)
        {
            UpgradePrice(purchaseItemType);
        }

    }
}
