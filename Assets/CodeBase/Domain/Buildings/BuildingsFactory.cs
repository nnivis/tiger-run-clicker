using System;
using System.Collections.Generic;
using UnityEngine;

namespace TigerClicker.CodeBase.Domain.Buildings
{
    [CreateAssetMenu(fileName = "BuildingsFactory", menuName = "Building/BuildingsFactory", order = 0)]
    public class BuildingsFactory : ScriptableObject
    {
        [SerializeField] private List<BuildingConfig> _configs;

        public Building Get(BuildingType buildingType)
        {
            BuildingConfig config = GetConfig(buildingType);
            Building building = Instantiate(config.Prefab);
            building.Initialize(config.BuildingType);
            return building;
        }

        private BuildingConfig GetConfig(BuildingType buildingType)
        {
            BuildingConfig config = _configs.Find(x => x.BuildingType == buildingType);
            if (config == null)
            {
                throw new ArgumentException("No configuration found for building type: " + buildingType);
            }
            return config;
        }
    }
}
