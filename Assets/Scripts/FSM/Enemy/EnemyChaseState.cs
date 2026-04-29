using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(IEnemyContext ctx) : base(ctx) { }
    
    private float _recalculateTimer = 0f;
    
    public override void Enter() { 
        Ctx.Agent.isStopped = false;
        Ctx.Agent.speed = Ctx.Stats.chaseSpeed; 
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
    }
}
