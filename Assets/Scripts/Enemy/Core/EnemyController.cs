using System;
using System.Collections;
using Enemy.States;
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
        
        public event Action<EnemyController> OnDespawnRequest;
        
        private float _baseSpeed;
        private Coroutine _slowCoroutine;
        
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
            
            _baseSpeed = stats.chaseSpeed;
            Agent.speed = _baseSpeed;
        }
        
        public void Despawn()
        {
            OnDespawnRequest?.Invoke(this);
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
        
        public void ApplySlow(float slowPercentage, float duration)
        {
            if (_slowCoroutine != null) 
            {
                StopCoroutine(_slowCoroutine);
            }
        
            _slowCoroutine = StartCoroutine(SlowRoutine(slowPercentage, duration));
        }

        private IEnumerator SlowRoutine(float slowPercentage, float duration)
        {
            Agent.speed = _baseSpeed * (1f - slowPercentage);
            
            yield return new WaitForSeconds(duration);

            Agent.speed = _baseSpeed;
        }
        
        public void ResetEnemy()
        {
            Health.Initialize(Stats.maxHealth);

            if (TryGetComponent(out Collider col)) col.enabled = true;
            if (TryGetComponent(out NavMeshAgent agent)) agent.enabled = true;

            StateMachine.ChangeState(new EnemyPatrolState(this));
            PlayerTarget = null;
            
            if (Agent != null) Agent.speed = _baseSpeed;
            if (_slowCoroutine != null) StopCoroutine(_slowCoroutine);
        }

        public void SetTarget(Transform target) => PlayerTarget = target;
    }
}
