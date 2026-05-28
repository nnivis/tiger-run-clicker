using System;
using UnityEngine;
using TigerClicker.CodeBase.Domain.Buildings;

namespace TigerClicker.CodeBase.Logick
{
    public class GamePlayMediator : MonoBehaviour
    {
        public Action<BuildingType, Vector3> OnLootDropped;
        public void NotifyLootDropped(BuildingType buildingType,  Vector3 position)
        {
            OnLootDropped?.Invoke(buildingType, position);
        }
    }
}
