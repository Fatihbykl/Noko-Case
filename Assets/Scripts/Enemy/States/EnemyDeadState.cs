using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(IEnemyContext ctx) : base(ctx) { }
    
    public override void Enter()
    {
        Ctx.Animator.SetTrigger(AnimHashes.DeadTrigger);
        Debug.Log("Enemy is Dead");
    }
}
