using UnityEngine;
using UnityEngine.AI;

public class EnemyFleeState : EnemyBaseState
{
    private float _recalculateTimer;
    public EnemyFleeState(IEnemyContext ctx) : base(ctx) { }

    public override void Tick()
    {
        _recalculateTimer += Time.deltaTime;
        if (_recalculateTimer >= Ctx.Stats.navMeshRecalculateDelay)
        {
            Vector3 fleeDirection = (Ctx.Transform.position - Ctx.PlayerTarget.position).normalized;
            Vector3 targetDestination = Ctx.Transform.position + (fleeDirection * 5.0f);
            if (NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                Ctx.Agent.SetDestination(hit.position);
            }
            _recalculateTimer = 0f;
        }

        // Belli bir mesafe açıldıysa tekrar devriyeye dön veya saklan
        if (Vector3.Distance(Ctx.Transform.position, Ctx.PlayerTarget.position) > 20f)
        {
            Ctx.StateMachine.ChangeState(new EnemyPatrolState(Ctx));
        }
    }

    public override void Enter()
    {
        Ctx.Agent.isStopped = false;
        Ctx.Agent.speed = Ctx.Stats.fleeSpeed;
        Ctx.Animator.SetBool(AnimHashes.IsMoving, true);
    }

    public override void Exit()
    {
        Ctx.Agent.isStopped = true;
        Ctx.Animator.SetBool(AnimHashes.IsMoving, false);
    }
}
