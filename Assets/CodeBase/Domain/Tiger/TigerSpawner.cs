using UnityEngine;
using TigerClicker.CodeBase.Logick;

namespace TigerClicker.CodeBase.Domain.Tiger
{
    public class TigerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _centrelPoint;
        [SerializeField] private TigerFactory _tigerFactory;
        [SerializeField] private GamePlayMediator _gamePlayMediator;
        public Tiger SpawnTiger()
        {
            Tiger tiger = _tigerFactory.Get(_centrelPoint.position);
            tiger.transform.position = _spawnPoint.position;
            tiger.OnLootDropped += (buildingType, position) => _gamePlayMediator.NotifyLootDropped(buildingType, position);
            return tiger;
        }

    }
}
