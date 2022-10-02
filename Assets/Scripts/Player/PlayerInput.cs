using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Joystick _joystick;
        private Vector3 _movingDirection = Vector3.zero;

        public Vector3 MovingDirection => _movingDirection;
        
        [Inject]
        public void Construct(Joystick joystick)
        {
            _joystick = joystick;
        }

        private void Update()
        {
            _movingDirection.x = _joystick.Direction.x;
            _movingDirection.z = _joystick.Direction.y;
        }
    }
}