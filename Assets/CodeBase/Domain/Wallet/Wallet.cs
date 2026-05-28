using System;
using TigerClicker.CodeBase.Data;

namespace TigerClicker.CodeBase.Domain
{
    public class Wallet
    {
        public event Action<long, CurrencyType> CurrencyChanged;
        private IPersistentData _persistentData;

        public Wallet(IPersistentData persistentData)
        {
            _persistentData = persistentData;
        }
        public void AddCurrency(int amount, CurrencyType currencyType)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            switch (currencyType)
            {
                case CurrencyType.Coin:
                    _persistentData.PlayerData.Coin += amount;
                    break;
                case CurrencyType.Meat:
                    _persistentData.PlayerData.Meat += amount;
                    break;
                default:
                    throw new ArgumentException(nameof(currencyType));
            }

            CurrencyChanged?.Invoke(GetCurrentCurrency(currencyType), currencyType);
        }

        public long GetCurrentCurrency(CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.Coin:
                    return _persistentData.PlayerData.Coin;
                case CurrencyType.Meat:
                    return _persistentData.PlayerData.Meat;
                default:
                    throw new ArgumentException(nameof(currencyType));
            }
        }

        public bool IsEnough(int amount, CurrencyType currencyType)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            switch (currencyType)
            {
                case CurrencyType.Coin:
                    return _persistentData.PlayerData.Coin >= amount;
                case CurrencyType.Meat:
                    return _persistentData.PlayerData.Meat >= amount;
                default:
                    throw new ArgumentException(nameof(currencyType));
            }
        }

        public void Spend(int amount, CurrencyType currencyType)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            switch (currencyType)
            {
                case CurrencyType.Coin:
                    _persistentData.PlayerData.Coin -= amount;
                    break;
                case CurrencyType.Meat:
                    _persistentData.PlayerData.Meat -= amount;
                    break;
                default:
                    throw new ArgumentException(nameof(currencyType));
            }

            CurrencyChanged?.Invoke(GetCurrentCurrency(currencyType), currencyType);
        }
    }
}
