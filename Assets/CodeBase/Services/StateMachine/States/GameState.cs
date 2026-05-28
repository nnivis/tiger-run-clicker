using UnityEngine;
using VContainer;
using TigerClicker.CodeBase.Data;
using TigerClicker.CodeBase.Domain;
using TigerClicker.CodeBase.Services.GameState;
using TigerClicker.CodeBase.Services.Visitors;

namespace TigerClicker.CodeBase.Services.StateMachine
{
    public class GameState : StateMachineBehavior
    {
        [SerializeField] private GamePresenter _gamePresenter;

        private Wallet _wallet;
        private PurchaseService _purchaseService;
        private PurchaseItemCheker _purchaseItemCheker;
        private PurchaseItemContent _purchaseItemContent;
        private IPersistentData _persistentData;

        [Inject]
        public void Construct(Wallet wallet, PurchaseService purchaseService,
            PurchaseItemCheker purchaseItemCheker, PurchaseItemContent purchaseItemContent,
            IPersistentData persistentData)
        {
            _wallet = wallet;
            _purchaseService = purchaseService;
            _purchaseItemCheker = purchaseItemCheker;
            _purchaseItemContent = purchaseItemContent;
            _persistentData = persistentData;
        }

        protected override void OnEnter()
        {
            _gamePresenter.Initialize(_wallet, _purchaseService, _purchaseItemCheker, _purchaseItemContent, _persistentData);
            _gamePresenter.StartWork();
        }

        protected override void OnExit()
        {
            _gamePresenter.StopWork();
        }
    }
}
