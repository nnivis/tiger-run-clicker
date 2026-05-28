using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TigerClicker.CodeBase.Services.GameState;

namespace TigerClicker.CodeBase.Data
{
    public class PlayerData
    {
        private Dictionary<PurchaseItemType, int> _purchaseItems;
        private int _coin;
        private int _meat;

        public PlayerData()
        {
            _coin = 1000;
            _meat = 1000;

            _purchaseItems = new Dictionary<PurchaseItemType, int>
{
    { PurchaseItemType.Buildings, 1 },
    { PurchaseItemType.Tiger, 1 }
};

        }

        [JsonConstructor]
        public PlayerData(int coin, int meat, Dictionary<PurchaseItemType, int> purchaseItems)
        {
            Coin = coin;
            Meat = meat;

            _purchaseItems = purchaseItems;
        }

        #region  Wallet
        public int Coin
        {
            get => _coin;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _coin = value;
            }
        }

        public int Meat
        {
            get => _meat;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _meat = value;
            }
        }
        #endregion

        #region  PurchaseItem

        public Dictionary<PurchaseItemType, int> PurchaseItems => _purchaseItems;
        public void UpdatePurchaseItemValue(PurchaseItemType itemType, int value)
        {
            if (!_purchaseItems.ContainsKey(itemType))
                throw new ArgumentException("Invalid item type");

            _purchaseItems[itemType] = value;
        }

        #endregion
    }
}
