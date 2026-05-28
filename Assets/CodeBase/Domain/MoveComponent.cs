
using UnityEngine;

namespace TigerClicker.CodeBase.Domain
{
    public class MoveComponent
    {
        public float InitialSpeed => _initialSpeed;
        private const float radiusX = 11;
        private const float radiusZ = 8;
        private float _speed;
        private float _initialSpeed;
        private float _angle;
        private Vector3 _centerPosition;
        

        public MoveComponent(float speed, Vector3 centerPosition)
        {
            _centerPosition = centerPosition;
            _speed = _initialSpeed = speed;
            _angle = 0f;
        }
        public void SetSpeed(float speed) => _speed = speed;
        public Vector3 NextPosition()
        {
            _angle += _speed * Time.deltaTime;

            float x = _centerPosition.x + Mathf.Cos(_angle) * radiusX;
            float z = _centerPosition.z + Mathf.Sin(_angle) * radiusZ;
            return new Vector3(x, _centerPosition.y, z);
        }

    }
}
