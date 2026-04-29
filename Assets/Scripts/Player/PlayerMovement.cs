using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController _controller;
        [SerializeField] private float _speed = 5f;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void Move(Vector2 input)
        {
            if (input.sqrMagnitude < 0.01f) return;

            Vector3 moveDirection = new Vector3(input.x, 0f, input.y).normalized;
            _controller.Move(moveDirection * (_speed * Time.deltaTime));
        
            transform.forward = moveDirection; 
        }
    }
}
