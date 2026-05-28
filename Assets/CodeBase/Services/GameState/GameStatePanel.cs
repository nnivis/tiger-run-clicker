using System;
using System.Collections.Generic;
using UnityEngine;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class GameStatePanel : MonoBehaviour
    {
        public event Action<PurchaseItemType, int> OnBuyNotify;
        public event Action OnSpeedUpNotify;

        [SerializeField] private SpeedUpButton _speedUpButton;
        [SerializeField] private Transform _spawnParent;
        [SerializeField] private PurchaseItemViewFactory _purchaseItemViewFactory;

        private List<PurchaseItemView> _purchaseItemViews = new List<PurchaseItemView>();

        public void Show(IEnumerable<PurchaseItem> purchaseItems)
        {
            Clear();
            foreach (PurchaseItem purchaseItem in purchaseItems)
            {
                PurchaseItemView view = _purchaseItemViewFactory.Get(purchaseItem, _spawnParent);
                _purchaseItemViews.Add(view);
                UpdatePurchaseItemStatus(view);
            }
            _speedUpButton.OnSpeedUpButtonClick += NotifySpeedIncrease;
        }

        public void UpdatePurchaseItemView(PurchaseItemType purchaseItemType)
        {
            foreach (PurchaseItemView view in _purchaseItemViews)
                if (view.PurchaseItem.PurchaseItemType == purchaseItemType)
                    UpdatePurchaseItemStatus(view);
        }

        private void UpdatePurchaseItemStatus(PurchaseItemView view)
        {
            bool isMaxed = view.PurchaseItem.PriceIncreaseCount >= view.PurchaseItem.PriceIncreaseCountLimit;

            view.OnBuyClick -= NotifyBuy;

            if (isMaxed)
            {
                view.DeactivateItem();
            }
            else
            {
                view.ActiveItem();
                view.OnBuyClick += NotifyBuy;
            }

            view.UpdatePriceText();
        }

        private void Clear()
        {
            _speedUpButton.OnSpeedUpButtonClick -= NotifySpeedIncrease;
            foreach (PurchaseItemView view in _purchaseItemViews)
                Destroy(view.gameObject);
            _purchaseItemViews.Clear();
        }

        private void NotifySpeedIncrease() => OnSpeedUpNotify?.Invoke();
        private void NotifyBuy(PurchaseItemType type, int amount) => OnBuyNotify?.Invoke(type, amount);
    }
}
