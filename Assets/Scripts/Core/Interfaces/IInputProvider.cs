using UnityEngine;

public interface IInputProvider
{
    Vector2 MovementInput { get; }
    bool IsAttacking { get; }
}