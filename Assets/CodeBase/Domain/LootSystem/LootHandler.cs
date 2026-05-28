using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using TigerClicker.CodeBase.Domain.Buildings;
using TigerClicker.CodeBase.Domain;
using TigerClicker.CodeBase.Infrastructure;
using TigerClicker.CodeBase.Logick;

namespace TigerClicker.CodeBase.Domain.LootSystem
{
    public class LootHandler : MonoBehaviour
    {
        [SerializeField] private GamePlayMediator _gamePlayMediator;
        [SerializeField] private LootView _lootView;
        private Dictionary<BuildingType, ILoot> _lootGenerators = new Dictionary<BuildingType, ILoot>();
        private IDataProvider _dataProvider;
        private Wallet _wallet;

        [Inject]
        public void Construct(IDataProvider dataProvider, Wallet wallet)
        {
            _dataProvider = dataProvider;
            _wallet = wallet;
            _gamePlayMediator.OnLootDropped += HandleBuildingCollision;
            InitializeLootGenerators();
        }
        private void InitializeLootGenerators()
        {
            _lootGenerators = new Dictionary<BuildingType, ILoot>
            {
                { BuildingType.Butchery, new CoinLoot() },
                { BuildingType.Bank, new MeatLoot() }
            };
        }
        private void HandleBuildingCollision(BuildingType buildingType, Vector3 position)
        {
            GenereteLoot(buildingType);
            GenereteLootView(buildingType, position);
        }
        private void GenereteLoot(BuildingType buildingType)
        {
            if (!_lootGenerators.ContainsKey(buildingType))
            {
                throw new ArgumentException("Unknown building type: " + buildingType);
            }
            _lootGenerators[buildingType].GenerateLoot(_wallet);

            _dataProvider.Save();
        }
        private void GenereteLootView(BuildingType buildingType, Vector3 position)
        {
            var yOffset = 6;
            Vector3 newPosition = new Vector3(position.x, position.y + yOffset, position.z);
            
            LootView lootView = Instantiate(_lootView, newPosition, Quaternion.identity);

            lootView.SetLootText(_lootGenerators[buildingType].Amount);
            lootView.Startwork();
        }

    }
}