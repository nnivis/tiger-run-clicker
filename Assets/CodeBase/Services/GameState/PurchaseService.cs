using System;
using System.Collections.Generic;
using TigerClicker.CodeBase.Data;
using TigerClicker.CodeBase.Domain;
using TigerClicker.CodeBase.Infrastructure;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class PurchaseService
    {
        private readonly Wallet _wallet;
        private readonly IDataProvider _dataProvider;
        private readonly IPersistentData _persistentData;
        private readonly PriceUpgrader _priceUpgrader;
        private readonly PurchaseItemContent _purchaseItemContent;

        private static readonly Dictionary<PurchaseItemType, CurrencyType> CurrencyMap =
            new Dictionary<PurchaseItemType, CurrencyType>
            {
                { PurchaseItemType.Tiger,     CurrencyType.Meat },
                { PurchaseItemType.Buildings, CurrencyType.Coin },
            };

        public PurchaseService(Wallet wallet, IDataProvider dataProvider, IPersistentData persistentData,
            PriceUpgrader priceUpgrader, PurchaseItemContent purchaseItemContent)
        {
            _wallet = wallet;
            _dataProvider = dataProvider;
            _persistentData = persistentData;
            _priceUpgrader = priceUpgrader;
            _purchaseItemContent = purchaseItemContent;
        }

        public bool TryBuy(PurchaseItemType type, int amount)
        {
            if (!CurrencyMap.TryGetValue(type, out CurrencyType currency))
                throw new ArgumentException($"Unknown purchase type: {type}");

            if (!_wallet.IsEnough(amount, currency))
                return false;

            _wallet.Spend(amount, currency);
            _priceUpgrader.Upgrade(type);

            PurchaseItem item = _purchaseItemContent.GetItemByType(type);
            _persistentData.PlayerData.UpdatePurchaseItemValue(type, item.PriceIncreaseCount);

            _dataProvider.Save();
            return true;
        }
    }
}
