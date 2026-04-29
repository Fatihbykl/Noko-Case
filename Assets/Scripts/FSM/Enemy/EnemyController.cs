using Data;
using FSM;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IEnemyContext
{
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }
    public Transform Transform => transform;
    public Transform PlayerTarget { get; private set; }
    public StateMachine StateMachine { get; private set; }
    
    [SerializeField] private EnemyData stats;
    public EnemyData Stats => stats;
    
    private int _currentHealth;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        StateMachine = new StateMachine();
    }

    private void Start()
    {
        _currentHealth = stats.maxHealth;
        StateMachine.ChangeState(new EnemyPatrolState(this));
    }

    private void Update() => StateMachine.Tick();

    public void TakeDamage(int damage, Transform attacker)
    {
        PlayerTarget = attacker;
        
        if (!stats.canAttack)
        {
            StateMachine.ChangeState(new EnemyFleeState(this));
        }
        else
        {
            StateMachine.ChangeState(new EnemyChaseState(this));
        }
    }

    public void SetTarget(Transform target) => PlayerTarget = target;
}
