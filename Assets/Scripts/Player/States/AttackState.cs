using FSM;
using UnityEngine;

public class AttackState : PlayerBaseState
{
    public AttackState(IPlayerContext context) : base(context) { }

    public override void Enter()
    {
        Ctx.Animator.SetTrigger(AnimHashes.AttackTrigger);
    }

    public override void Tick()
    {
        Ctx.Movement.Move(Ctx.Input.MovementInput);

        Ctx.Combat.CastAoEBasicAttack();

        if (!Ctx.Input.IsAttacking)
        {
            if (Ctx.Input.MovementInput.sqrMagnitude > 0.1f)
                Ctx.StateMachine.ChangeState(new MoveState(Ctx));
            else
                Ctx.StateMachine.ChangeState(new IdleState(Ctx));
        }
    }
}
