using Player.Components;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController _controller;
        private PlayerStats _playerStats;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _playerStats = GetComponent<PlayerStats>();
        }

        public void Move(Vector2 input)
        {
            if (input.sqrMagnitude < 0.01f) return;

            Vector3 moveDirection = new Vector3(input.x, 0f, input.y).normalized;
            _controller.Move(moveDirection * (_playerStats.MoveSpeed.GetValue() * Time.deltaTime));
        
            transform.forward = moveDirection; 
        }
    }
}
