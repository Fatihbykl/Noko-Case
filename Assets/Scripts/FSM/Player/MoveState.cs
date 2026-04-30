using FSM;
using UnityEngine;

public class MoveState : PlayerBaseState
{
    public MoveState(IPlayerContext context) : base(context) { }

    public override void Enter()
    {
        Ctx.Animator.SetBool(AnimHashes.IsMoving, true);
    }

    public override void Tick()
    {
        if (Ctx.Input.IsAttacking)
        {
            Ctx.StateMachine.ChangeState(new AttackState(Ctx));
            return;
        }

        if (Ctx.Input.MovementInput.sqrMagnitude <= 0.1f)
        {
            Ctx.StateMachine.ChangeState(new IdleState(Ctx));
            Ctx.Animator.SetBool(AnimHashes.IsMoving, false);
            return;
        }
        
        Ctx.Movement.Move(Ctx.Input.MovementInput);
    }
}
