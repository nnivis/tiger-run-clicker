using UnityEngine;

namespace TigerClicker.CodeBase.Services.GameState
{
    [CreateAssetMenu(fileName = "PurchaseItem", menuName = "PurchaseItem/PurchaseItem", order = 0)]
    public class PurchaseItem : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private int _price;
        [SerializeField] private int _priceIncreaseCount;
        [SerializeField] private int _priceIncreaseCountLimit;
        [SerializeField] private PurchaseItemType _purchaseItemType;

        public Sprite Icon => _icon;
        public string Name => _name;
        public int Price => _price;
        public int PriceIncreaseCount => _priceIncreaseCount;
        public int PriceIncreaseCountLimit => _priceIncreaseCountLimit;
        public PurchaseItemType PurchaseItemType => _purchaseItemType;

        public void UpdatePrice(int newPrice)
        {
            _price = newPrice;
        }

        public void UpdatePriceIncreaseCount(int newValue)
        {
            _priceIncreaseCount = newValue;
        }
    }
}
