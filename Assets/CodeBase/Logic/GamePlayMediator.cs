using System;
using UnityEngine;
using TigerClicker.CodeBase.Domain.Buildings;

namespace TigerClicker.CodeBase.Logic
{
    public class GamePlayMediator : MonoBehaviour
    {
        public event Action<BuildingType, Vector3> OnLootDropped;

        public void NotifyLootDropped(BuildingType buildingType, Vector3 position) =>
            OnLootDropped?.Invoke(buildingType, position);
    }
}
