using System;
using System.Collections.Generic;
using UnityEngine;

namespace TigerClicker.CodeBase.Domain.Tiger
{
    [CreateAssetMenu(fileName = "TigerFactory", menuName = "Tiger/TigerFactory", order = 0)]
    public class TigerFactory : ScriptableObject
    {
        [SerializeField] private List<TigerConfig> _configs;

        public Tiger Get(Vector3 centerPosition)
        {
            TigerConfig config = GetConfig();
            Tiger tiger = Instantiate(config.Prefab);
            tiger.Initialize(config.Speed, centerPosition);
            return tiger;
        }

        private TigerConfig GetConfig()
        {
            if (_configs.Count == 0)
            {
                throw new ArgumentException("The List is empty");
            }

            int randomIndex = UnityEngine.Random.Range(0, _configs.Count);
            TigerConfig config = _configs[randomIndex];
            return config;
        }
    }
}
