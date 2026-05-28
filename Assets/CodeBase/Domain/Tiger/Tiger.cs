using System;
using System.Collections;
using UnityEngine;
using TigerClicker.CodeBase.Domain;
using TigerClicker.CodeBase.Domain.Buildings;

namespace TigerClicker.CodeBase.Domain.Tiger
{
    [RequireComponent(typeof(Rigidbody))]
    public class Tiger : MonoBehaviour
    {
        public event Action<BuildingType, Vector3> OnLootDropped;
        private MoveComponent _moveComponent;
        private Coroutine _speedBoostCoroutine;
        private bool _isTriggerExited = false;
        private const float duration = 0.5f;

        public void Initialize(float speed, Vector3 centerPosition)
        {
            _moveComponent = new MoveComponent(speed, centerPosition);
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 newPosition = _moveComponent.NextPosition();
            transform.position = newPosition;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isTriggerExited)
            {
                _isTriggerExited = true;
                Building building = other.GetComponent<Building>();
                if (building != null)
                {
                    BuildingType buildingType = building.BuildingType;
                    OnLootDropped?.Invoke(buildingType, transform.position);
                    StartCoroutine(ResetTrigger());
                }
            }
        }

        private IEnumerator ResetTrigger()
        {
            yield return new WaitForSeconds(duration);
            _isTriggerExited = false;
        }

        private IEnumerator IncreaseSpeedForDuration()
        {
            float randomMultiplier = UnityEngine.Random.Range(1.5f, 2f);
            _moveComponent.SetSpeed(_moveComponent.InitialSpeed * randomMultiplier);
            yield return new WaitForSeconds(duration);
            _moveComponent.SetSpeed(_moveComponent.InitialSpeed);
        }
        public void ActivateSpeedBoost()
        {
            if (_speedBoostCoroutine != null)
            {
                StopCoroutine(_speedBoostCoroutine);
            }
            _speedBoostCoroutine = StartCoroutine(IncreaseSpeedForDuration());
        }

    }
}
