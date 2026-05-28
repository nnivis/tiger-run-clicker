using System;
using System.Collections.Generic;
using UnityEngine;
using TigerClicker.CodeBase.Services.Visitors;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class GameStatePanel : MonoBehaviour
    {
        public event Action<PurchaseItemType, int> OnBuyNotify;
        public event Action OnSpeedUpNotify;
        [SerializeField] private SpeedUpButton _speedUpButton;
        [SerializeField] private Transform _spawnParent;
        [SerializeField] private PurchaseItemViewFactory _purchaseItemViewFactory;
        private PurchaseItemCheker _purchaseItemCheker;
        private List<PurchaseItemView> _purchaseItemView = new List<PurchaseItemView>();

        public void Initialize(PurchaseItemCheker purchaseItemCheker)
        {
            _purchaseItemCheker = purchaseItemCheker;
        }
        private void UpdatePurchaseItemStatus(PurchaseItemView purchaseItemView)
        {
            _purchaseItemCheker.Visit(purchaseItemView.PurchaseItem);
            if (purchaseItemView.PurchaseItem.PriceIncreaseCount == purchaseItemView.PurchaseItem.PriceIncreaseCountLimit)
            {
                purchaseItemView.DeactivateItem();
                purchaseItemView.OnBuyClick -= NotifyBuy;
            }
            else
            {
                purchaseItemView.ActiveItem();
                 purchaseItemView.OnBuyClick += NotifyBuy;
            }
            purchaseItemView.UpdatePriceText();
        }
        private void Clear()
        {
            _speedUpButton.OnSpeedUpButtonClick -= NotifySpeedIncrease;
            foreach (PurchaseItemView purchaseItemView in _purchaseItemView)
            {
                Destroy(purchaseItemView.gameObject);
            }
            _purchaseItemView.Clear();
        }
        private void NotifySpeedIncrease()
        {
            OnSpeedUpNotify();
        }

        private void NotifyBuy(PurchaseItemType type, int amount)
        {
            OnBuyNotify(type, amount);
        }
        public void Show(IEnumerable<PurchaseItem> purchaseItems)
        {
            Clear();
            foreach (PurchaseItem purchaseItem in purchaseItems)
            {
                PurchaseItemView spawnedPurchaseItemView = _purchaseItemViewFactory.Get(purchaseItem, _spawnParent);
                _purchaseItemView.Add(spawnedPurchaseItemView);

                UpdatePurchaseItemStatus(spawnedPurchaseItemView);
            }
            _speedUpButton.OnSpeedUpButtonClick += NotifySpeedIncrease;
        }

        public void UpdatePurchaseItemView(PurchaseItemType purchaseItemType)
        {
            foreach (PurchaseItemView purchaseItemView in _purchaseItemView)
            {
                if (purchaseItemView.PurchaseItem.PurchaseItemType == purchaseItemType)
                {
                    UpdatePurchaseItemStatus(purchaseItemView);
                }
            }
        }

    }
}
