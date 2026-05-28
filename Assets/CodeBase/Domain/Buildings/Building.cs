using UnityEngine;

namespace TigerClicker.CodeBase.Domain.Buildings
{
    public class Building : MonoBehaviour
    {
        public BuildingType BuildingType => _buildingType;
        private BuildingType _buildingType;
        public void Initialize(BuildingType buildingType)
        {
            _buildingType = buildingType;
        }
    }
}
