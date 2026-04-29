using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FSM
{
    public interface IPlayerContext
    {
        PlayerMovement Movement { get; }
        PlayerCombat Combat { get; }
        IInputProvider Input { get; }
        StateMachine StateMachine { get; }
        Animator Animator { get; }
    }
}
