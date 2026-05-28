using UnityEngine;
using VContainer;
using TigerClicker.CodeBase.Data;
using TigerClicker.CodeBase.Services.GameState;

namespace TigerClicker.CodeBase.Infrastructure
{
    public class BootstrapComponents : MonoBehaviour
    {
        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;
        private PriceUpgrader _priceUpgrader;

        [Inject]
        public void Construct(IDataProvider dataProvider, IPersistentData persistentData, PriceUpgrader priceUpgrader)
        {
            _dataProvider = dataProvider;
            _persistentData = persistentData;
            _priceUpgrader = priceUpgrader;
        }

        private void Start()
        {
            if (!_dataProvider.TryLoad())
                _persistentData.PlayerData = new PlayerData();

            _priceUpgrader.Initialize();
        }
    }
}
