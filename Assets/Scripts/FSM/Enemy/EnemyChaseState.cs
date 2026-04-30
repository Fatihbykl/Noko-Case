using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(IEnemyContext ctx) : base(ctx) { }
    
    private float _recalculateTimer;
    
    public override void Enter() { 
        Ctx.Agent.isStopped = false;
        Ctx.Agent.speed = Ctx.Stats.chaseSpeed;
        Ctx.Animator.SetBool(AnimHashes.IsMoving, true);
    }

    public override void Tick()
    {
        if (Ctx.PlayerTarget == null) return;

        float distance = Vector3.Distance(Ctx.Transform.position, Ctx.PlayerTarget.position);

        if (distance <= Ctx.Stats.attackRange)
        {
            Ctx.StateMachine.ChangeState(new EnemyAttackState(Ctx));
            return;
        }

        // Optimizasyon: Yolu her kare yerine belirli aralıklarla güncelle
        _recalculateTimer += Time.deltaTime;
        if (_recalculateTimer >= Ctx.Stats.navMeshRecalculateDelay)
        {
            Ctx.Agent.SetDestination(Ctx.PlayerTarget.position);
            _recalculateTimer = 0f;
        }
    }


    public override void Exit()
    {
        Ctx.Agent.isStopped = true;
        Ctx.Animator.SetBool(AnimHashes.IsMoving, false);
    }
}
