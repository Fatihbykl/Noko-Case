using System;
using FSM;
using Player.Components;
using Systems.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Core
{
    public class EnemyController : MonoBehaviour, IEnemyContext
    {
        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }
        public Transform Transform => transform;
        public Transform PlayerTarget { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public HealthSystem Health { get; private set; }
    
        [SerializeField] private EnemyData stats;
        public EnemyData Stats => stats;
        
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponentInChildren<Animator>();
            StateMachine = new StateMachine();
            Health = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            Health.Initialize(stats.maxHealth);
            StateMachine.ChangeState(new EnemyPatrolState(this));
        }

        private void OnEnable()
        {
            Health.OnDamaged += AlertCheck;
            Health.OnDied += HandleDeath;
        }

        private void OnDisable()
        {
            Health.OnDied -= HandleDeath;
            Health.OnDamaged -= AlertCheck;
        }

        private void Update() => StateMachine.Tick();

        private void HandleDeath()
        {
            if (StateMachine.CurrentState is not EnemyDeadState)
            {
                StateMachine.ChangeState(new EnemyDeadState(this));
                if (PlayerTarget != null && PlayerTarget.TryGetComponent(out PlayerWallet wallet))
                {
                    wallet.AddGold(stats.goldReward);
                }
            }
        }

        private void AlertCheck(Transform attacker)
        {
            PlayerTarget = attacker;
        
            if(StateMachine.CurrentState is not EnemyPatrolState) return;
            if (!stats.canAttack)
            {
                StateMachine.ChangeState(new EnemyFleeState(this));
            }
            else
            {
                StateMachine.ChangeState(new EnemyChaseState(this));
            }
        }
        
        public void ResetEnemy()
        {
            Health.Initialize(stats.maxHealth);

            if (TryGetComponent(out NavMeshAgent agent))
            {
                agent.ResetPath();
                agent.velocity = Vector3.zero;
            }

            StateMachine.ChangeState(new EnemyPatrolState(this));
            PlayerTarget = null;
        }

        public void SetTarget(Transform target) => PlayerTarget = target;
    }
}
