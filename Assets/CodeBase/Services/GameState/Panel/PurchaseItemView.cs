using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class PurchaseItemView : MonoBehaviour
    {
        public event Action<PurchaseItemType, int> OnBuyClick;
        public PurchaseItem PurchaseItem => _purchaseItem;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _maxText;
        [SerializeField] private Button _button;
        private PurchaseItem _purchaseItem;
        public void Initialize(PurchaseItem purchaseItem)
        {
            _purchaseItem = purchaseItem;
            _icon.sprite = purchaseItem.Icon;
            _nameText.text = purchaseItem.Name;
            
            UpdatePriceText();
        }

        public void UpdatePriceText()
        {
            _priceText.text = _purchaseItem.Price.ToString();
        }
        public void ActiveItem()
        {
            _maxText.gameObject.SetActive(false);

            _icon.gameObject.SetActive(true);
            _priceText.gameObject.SetActive(true);
        }
        public void DeactivateItem()
        {
            _maxText.gameObject.SetActive(true);

            _icon.gameObject.SetActive(false);
            _priceText.gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(OnBuyBuildingsButtonClicked);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnBuyBuildingsButtonClicked);
        }

        private void OnBuyBuildingsButtonClicked()
        {
            if (OnBuyClick != null)
                OnBuyClick(_purchaseItem.PurchaseItemType, _purchaseItem.Price);
        }

    }
}
