using System;
using System.Collections.Generic;
using TigerClicker.CodeBase.Domain;
using TigerClicker.CodeBase.Infrastructure;
using TigerClicker.CodeBase.Services.Visitors;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class PurchaseService
    {
        private readonly Wallet _wallet;
        private readonly IDataProvider _dataProvider;
        private readonly PriceUpgrader _priceUpgrader;
        private readonly PurchaseItemUpdater _purchaseItemUpdater;
        private readonly PurchaseItemContent _purchaseItemContent;

        private static readonly Dictionary<PurchaseItemType, CurrencyType> CurrencyMap =
            new Dictionary<PurchaseItemType, CurrencyType>
            {
                { PurchaseItemType.Tiger,     CurrencyType.Meat },
                { PurchaseItemType.Buildings, CurrencyType.Coin },
            };

        public PurchaseService(Wallet wallet, IDataProvider dataProvider, PriceUpgrader priceUpgrader,
            PurchaseItemUpdater purchaseItemUpdater, PurchaseItemContent purchaseItemContent)
        {
            _wallet = wallet;
            _dataProvider = dataProvider;
            _priceUpgrader = priceUpgrader;
            _purchaseItemUpdater = purchaseItemUpdater;
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
            _purchaseItemUpdater.Visit(_purchaseItemContent.GetItemByType(type));
            _dataProvider.Save();
            return true;
        }
    }
}
