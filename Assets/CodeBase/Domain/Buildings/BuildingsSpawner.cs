using System;
using System.Collections.Generic;
using UnityEngine;

namespace TigerClicker.CodeBase.Domain.Buildings
{
    public class BuildingsSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private BuildingsFactory _buildingsFactory;
        private List<Transform> _usedSpawnPoints = new List<Transform>();

        public Building SpawnBuilding(BuildingType buildingType)
        {
            if (_usedSpawnPoints.Count == 0)
            {
                throw new InvalidOperationException("No spawn points available.");
            }

            int randomIndex = UnityEngine.Random.Range(0, _usedSpawnPoints.Count);
            Transform spawnPoint = _usedSpawnPoints[randomIndex];
            _usedSpawnPoints.RemoveAt(randomIndex);

            Building building = _buildingsFactory.Get(buildingType);
            building.transform.position = spawnPoint.position;
            return building;
        }

        public void ResetSpawnPoints()
        {
            _usedSpawnPoints.Clear();
            _usedSpawnPoints.AddRange(_spawnPoints);
        }
    }
}
