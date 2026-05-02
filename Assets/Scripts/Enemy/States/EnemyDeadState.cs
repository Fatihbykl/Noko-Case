using Enemy.Core;
using UnityEngine;

namespace Enemy.States
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(IEnemyContext ctx) : base(ctx) { }
    
        private EnemyController _controller;
        private float _despawnTimer;
        private float _timeToDespawn = 2f;

        public override void Enter()
        {
            _controller = Ctx.Transform.GetComponent<EnemyController>();
            _despawnTimer = 0f;

            Ctx.Animator.SetTrigger(AnimHashes.DeadTrigger);

            if (_controller.TryGetComponent(out Collider col)) col.enabled = false;
            if (_controller.TryGetComponent(out UnityEngine.AI.NavMeshAgent agent)) agent.enabled = false;
        }

        public override void Tick()
        {
            _despawnTimer += Time.deltaTime;

            if (_despawnTimer >= _timeToDespawn)
            {
                _controller.Despawn();
                Debug.Log("Despawn");
            }
        }
    }
}
