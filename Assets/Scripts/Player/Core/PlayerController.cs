using System;
using Enemy.States;
using FSM;
using Player.Components;
using Systems.Combat;
using UnityEngine;

namespace Player.Core
{
    public class PlayerController : MonoBehaviour, IPlayerContext
    {
        public PlayerMovement Movement { get; private set; }
        public PlayerCombat Combat { get; private set; }
        public IInputProvider Input { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public Animator Animator { get; private set; }
        public HealthSystem Health { get; private set; }

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            
            Movement = GetComponent<PlayerMovement>();
            Combat = GetComponent<PlayerCombat>();
            Input = GetComponent<IInputProvider>();
            StateMachine = new StateMachine();
            Animator = GetComponent<Animator>();
            Health = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            StateMachine.ChangeState(new IdleState(this));
        }

        private void OnEnable()
        {
            Health.OnDied += HandleDeath;
        }

        private void OnDisable()
        {
            Health.OnDied -= HandleDeath;
        }

        private void Update()
        {
            StateMachine.Tick();
        }

        private void HandleDeath()
        {
            if (StateMachine.CurrentState is not EnemyDeadState)
            {
                StateMachine.ChangeState(new DeadState(this));
            }
        }
    }
}
