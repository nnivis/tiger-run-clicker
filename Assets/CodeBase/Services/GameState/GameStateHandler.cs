using System.Collections.Generic;
using UnityEngine;
using TigerClicker.CodeBase.Data;
using TigerClicker.CodeBase.Domain.Buildings;
using TigerClicker.CodeBase.Domain.Tiger;
using TigerClicker.CodeBase.Domain;
using TigerClicker.CodeBase.Infrastructure;
using TigerClicker.CodeBase.Services.Visitors;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class GameStateHandler : MonoBehaviour
    {
        [SerializeField] private GameStatePanel _gameStatePanel;
        [SerializeField] private TigerSpawner _tigerSpawner;
        [SerializeField] private BuildingsSpawner _buildingsSpawner;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private PurchaseItemContent _purchaseItemContent;
        private PriceUpgrader _priceUpgrader;
        private PurchaseItemCheker _purchaseItemCheker;
        private PurchaseItemUpdater _purchaseItemUpdater;
        private List<Tiger> _tigersList = new List<Tiger>();
        private List<Building> _buildingsList = new List<Building>();
        private Wallet _wallet;
        private IDataProvider _dataProvider;

        public void Initialize(IDataProvider dataProvider, Wallet wallet, PurchaseItemCheker purchaseItemCheker, PurchaseItemUpdater purchaseItemUpdater)
        {
            _dataProvider = dataProvider;

            _wallet = wallet;
            _walletView.Initialize(_wallet);

            _purchaseItemCheker = purchaseItemCheker;
            _purchaseItemUpdater = purchaseItemUpdater;
            _priceUpgrader = new PriceUpgrader(_purchaseItemContent.PurchaseItems, _purchaseItemCheker);

            _gameStatePanel.Initialize(_purchaseItemCheker);
            _gameStatePanel.OnBuyNotify += BuyItem;
            _gameStatePanel.OnSpeedUpNotify += SpeedUpTiger;
        }
        private void SpeedUpTiger()
        {
            foreach (Tiger tiger in _tigersList)
            {
                tiger.ActivateSpeedBoost();
            }
        }

        private void BuyItem(PurchaseItemType purchaseItemType, int amount)
        {
            switch (purchaseItemType)
            {
                case PurchaseItemType.Tiger:
                    BuyTiger(purchaseItemType, amount);
                    break;
                case PurchaseItemType.Buildings:
                    BuyButchery(purchaseItemType, amount);
                    break;
            }
        }

        private void BuyTiger(PurchaseItemType purchaseItemType, int amount)
        {
            if (_wallet.IsEnough(amount, CurrencyType.Meat))
            {
                _wallet.Spend(amount, CurrencyType.Meat);
                SpawnTiger();
                _priceUpgrader.UpgradePriceForTiger(purchaseItemType);
                _purchaseItemUpdater.Visit(_purchaseItemContent.GetItemByType(purchaseItemType));
                UpdatePurchaseItemView(purchaseItemType);

                _dataProvider.Save();
            }
        }

        private void BuyButchery(PurchaseItemType purchaseItemType, int amount)
        {
            if (_wallet.IsEnough(amount, CurrencyType.Coin))
            {
                _wallet.Spend(amount, CurrencyType.Coin);
                SpawnBuilding(BuildingType.Butchery);
                _priceUpgrader.UpgradePriceForButchery(purchaseItemType);
                _purchaseItemUpdater.Visit(_purchaseItemContent.GetItemByType(purchaseItemType));
                UpdatePurchaseItemView(purchaseItemType);

                _dataProvider.Save();
            }
        }
        private void SpawnTiger()
        {
            Tiger tiger = _tigerSpawner.SpawnTiger();
            _tigersList.Add(tiger);
        }

        private void SpawnBuilding(BuildingType buildingType)
        {
            Building building = _buildingsSpawner.SpawnBuilding(buildingType);
            _buildingsList.Add(building);
        }

        private void UpdatePurchaseItemView(PurchaseItemType purchaseItemType)
        {
            _gameStatePanel.UpdatePurchaseItemView(purchaseItemType);
        }
        public void StartWork()
        {
            SpawnTiger();

            _buildingsSpawner.ResetSpawnPoints();
            SpawnBuilding(BuildingType.Butchery);
            SpawnBuilding(BuildingType.Bank);

            _gameStatePanel.Show(_purchaseItemContent.PurchaseItems);
        }

        public void StopWork()
        {
            foreach (Tiger tiger in _tigersList)
            {
                if (tiger != null)
                {
                    Destroy(tiger.gameObject);
                }
            }
            _tigersList.Clear();

            foreach (Building building in _buildingsList)
            {
                if (building != null)
                {
                    Destroy(building.gameObject);
                }
            }
            _buildingsList.Clear();
        }

    }
}
