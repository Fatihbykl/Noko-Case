using Enemy.Core;
using FSM;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyContext
{
    NavMeshAgent Agent { get; }
    Animator Animator { get; }
    Transform Transform { get; }
    Transform PlayerTarget { get; }
    StateMachine StateMachine { get; }
    EnemyData Stats { get; }
    
    void SetTarget(Transform target);
}
