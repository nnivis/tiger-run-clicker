using TMPro;
using UnityEngine;

namespace TigerClicker.CodeBase.Domain
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinValue;
        [SerializeField] private TMP_Text _meatValue;
        private Wallet _wallet;

        public void Initialize(Wallet wallet)
        {
            _wallet = wallet;

            UpdateValues(_wallet.GetCurrentCurrency(CurrencyType.Coin), CurrencyType.Coin);
            UpdateValues(_wallet.GetCurrentCurrency(CurrencyType.Meat), CurrencyType.Meat);

            _wallet.CurrencyChanged += UpdateValues;
        }

        private void OnDestroy()
        {
            _wallet.CurrencyChanged -= UpdateValues;
        }


        private void UpdateValues(long value, CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.Coin:
                    _coinValue.text = value.ToString();
                    break;
                case CurrencyType.Meat:
                    _meatValue.text = value.ToString();
                    break;
            }
        }
    }
}
