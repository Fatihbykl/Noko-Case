using FSM;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(IPlayerContext context) : base(context) { }

    public override void Tick()
    {
        if (Ctx.Input.MovementInput.sqrMagnitude > 0.1f)
        {
            Ctx.StateMachine.ChangeState(new MoveState(Ctx));
        }
    }
}
