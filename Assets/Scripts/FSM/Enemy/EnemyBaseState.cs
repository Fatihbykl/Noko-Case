using FSM;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected readonly IEnemyContext Ctx;

    protected EnemyBaseState(IEnemyContext context)
    {
        Ctx = context;
    }

    public virtual void Enter() { }
    public virtual void Tick() { }
    public virtual void Exit() { }
}
