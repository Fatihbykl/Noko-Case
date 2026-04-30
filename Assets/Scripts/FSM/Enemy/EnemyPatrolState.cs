using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    private Vector3 _destination;

    public EnemyPatrolState(IEnemyContext ctx) : base(ctx) { }

    public override void Enter()
    {
        Ctx.Agent.isStopped = false;
        Ctx.Agent.speed = Ctx.Stats.patrolSpeed;
        Ctx.Animator.SetBool(AnimHashes.IsMoving, true);
        
        SetNewDestination();
    }

    public override void Tick()
    {
        if (!Ctx.Agent.pathPending && Ctx.Agent.remainingDistance <= Ctx.Agent.stoppingDistance + 0.1f)
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        Vector2 randomDir = Random.insideUnitCircle * Ctx.Stats.patrolRadius;
        Vector3 randomPos = Ctx.Transform.position + new Vector3(randomDir.x, 0, randomDir.y);

        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, Ctx.Stats.patrolRadius, NavMesh.AllAreas))
        {
            Ctx.Agent.SetDestination(hit.position);
        }
    }

    public override void Exit()
    {
        Ctx.Agent.isStopped = true;
        Ctx.Animator.SetBool(AnimHashes.IsMoving, false);
    }
}
