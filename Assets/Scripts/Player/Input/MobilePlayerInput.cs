using UI;
using UnityEngine;

namespace Player.Input
{
    public class MobilePlayerInput : MonoBehaviour, IInputProvider
    {
        [Header("References")]
        [SerializeField] private FloatingJoystick _joystick;

        public Vector2 MovementInput => _joystick.InputVector;
    
        public bool IsAttacking => false; 
    }
}