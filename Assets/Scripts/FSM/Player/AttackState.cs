using FSM;
using UnityEngine;

public class AttackState : PlayerBaseState
{
    private static readonly int AttackTrigger = Animator.StringToHash("AttackTrigger");
    public AttackState(IPlayerContext context) : base(context) { }

    public override void Enter()
    {
        Ctx.Animator.SetTrigger(AttackTrigger);
    }

    public override void Tick()
    {
        // Karakter ateş ederken de hareket edebilsin! (Run & Gun)
        Ctx.Movement.Move(Ctx.Input.MovementInput);

        // Büyü fırlat
        Ctx.Combat.CastSpell();

        // Ateş etme tuşu bırakıldıysa durumdan çık
        if (!Ctx.Input.IsAttacking)
        {
            if (Ctx.Input.MovementInput.sqrMagnitude > 0.1f)
                Ctx.StateMachine.ChangeState(new MoveState(Ctx));
            else
                Ctx.StateMachine.ChangeState(new IdleState(Ctx));
        }
    }
}
