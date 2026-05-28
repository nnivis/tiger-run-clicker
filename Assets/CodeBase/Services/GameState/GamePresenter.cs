using UnityEngine;
using TigerClicker.CodeBase.Data;
using TigerClicker.CodeBase.Domain;
using TigerClicker.CodeBase.Domain.Buildings;
using TigerClicker.CodeBase.Services.Visitors;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private GameStatePanel _gameStatePanel;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private GameEntityManager _entityManager;

        private PurchaseService _purchaseService;
        private PurchaseItemContent _purchaseItemContent;
        private IPersistentData _persistentData;
        private bool _isInitialized;

        public void Initialize(Wallet wallet, PurchaseService purchaseService,
            PurchaseItemCheker purchaseItemCheker, PurchaseItemContent purchaseItemContent,
            IPersistentData persistentData)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            _purchaseService = purchaseService;
            _purchaseItemContent = purchaseItemContent;
            _persistentData = persistentData;

            _walletView.Initialize(wallet);
            _gameStatePanel.Initialize(purchaseItemCheker);
            _gameStatePanel.OnBuyNotify += HandleBuy;
            _gameStatePanel.OnSpeedUpNotify += _entityManager.SpeedUpAll;
        }

        public void StartWork()
        {
            var purchases = _persistentData.PlayerData.PurchaseItems;

            int tigerCount = purchases.TryGetValue(PurchaseItemType.Tiger, out int t) ? t : 1;
            int butcheryCount = purchases.TryGetValue(PurchaseItemType.Buildings, out int b) ? b : 1;

            _entityManager.StartWork(tigerCount, butcheryCount);
            _gameStatePanel.Show(_purchaseItemContent.PurchaseItems);
        }

        public void StopWork() => _entityManager.StopWork();

        private void HandleBuy(PurchaseItemType type, int amount)
        {
            if (!_purchaseService.TryBuy(type, amount)) return;

            switch (type)
            {
                case PurchaseItemType.Tiger:     _entityManager.SpawnTiger(); break;
                case PurchaseItemType.Buildings: _entityManager.SpawnBuilding(BuildingType.Butchery); break;
            }

            _gameStatePanel.UpdatePurchaseItemView(type);
        }
    }
}
