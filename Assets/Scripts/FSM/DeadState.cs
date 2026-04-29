using FSM;
using UnityEngine;

public class DeadState : PlayerBaseState
{
    public DeadState(IPlayerContext context) : base(context) { }

    public override void Enter()
    {
        Debug.Log("Player is Dead");
    }
}
