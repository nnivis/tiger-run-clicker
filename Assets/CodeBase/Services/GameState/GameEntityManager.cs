using System.Collections.Generic;
using UnityEngine;
using TigerClicker.CodeBase.Domain.Buildings;
using TigerClicker.CodeBase.Domain.Tiger;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class GameEntityManager : MonoBehaviour
    {
        [SerializeField] private TigerSpawner _tigerSpawner;
        [SerializeField] private BuildingsSpawner _buildingsSpawner;

        private readonly List<Tiger> _tigers = new List<Tiger>();
        private readonly List<Building> _buildings = new List<Building>();

        public void StartWork(int tigerCount, int butcheryCount)
        {
            _buildingsSpawner.ResetSpawnPoints();

            for (int i = 0; i < tigerCount; i++)
                SpawnTiger();

            for (int i = 0; i < butcheryCount; i++)
                SpawnBuilding(BuildingType.Butchery);

            SpawnBuilding(BuildingType.Bank);
        }

        public void StopWork()
        {
            foreach (Tiger tiger in _tigers)
                if (tiger != null)
                    Destroy(tiger.gameObject);
            _tigers.Clear();

            foreach (Building building in _buildings)
                if (building != null)
                    Destroy(building.gameObject);
            _buildings.Clear();
        }

        public void SpeedUpAll()
        {
            foreach (Tiger tiger in _tigers)
                tiger.ActivateSpeedBoost();
        }

        public void SpawnTiger()
        {
            Tiger tiger = _tigerSpawner.SpawnTiger();
            _tigers.Add(tiger);
        }

        public void SpawnBuilding(BuildingType buildingType)
        {
            Building building = _buildingsSpawner.SpawnBuilding(buildingType);
            _buildings.Add(building);
        }
    }
}
