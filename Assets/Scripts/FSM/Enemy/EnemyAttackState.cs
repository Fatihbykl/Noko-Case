using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(IEnemyContext ctx) : base(ctx) { }

    public override void Enter()
    {
        Ctx.Animator.SetTrigger(AnimHashes.AttackTrigger);
    }
}
