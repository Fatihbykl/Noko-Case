using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProvider : MonoBehaviour, IInputProvider
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _attackAction;

    public Vector2 MovementInput { get; private set; }
    public bool IsAttacking { get; private set; }

    private void OnEnable()
    {
        _moveAction.action.Enable();
        _attackAction.action.Enable();
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
        _attackAction.action.Disable();
    }

    private void Update()
    {
        MovementInput = _moveAction.action.ReadValue<Vector2>();
        IsAttacking = _attackAction.action.IsPressed();
    }
}
