using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using VFX;

namespace Player.Components
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float attackCooldown = 2f;
        
        [Header("VFX Settings")] 
        [SerializeField] private SpellVFX spellPrefab;
        [SerializeField] private float vfxSpawnHeight = 3f;

        private float _nextAttackTime;
        private Collider[] _hitColliders = new Collider[20];
        private PlayerStats _playerStats;
        private ObjectPool<SpellVFX> _vfxPool;

        private void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
            _vfxPool = new ObjectPool<SpellVFX>(
                createFunc: () =>
                {
                    var vfx = Instantiate(spellPrefab);
                    vfx.SetPool(_vfxPool);
                    return vfx;
                },
                actionOnGet: vfx => vfx.gameObject.SetActive(true),
                actionOnRelease: vfx => vfx.gameObject.SetActive(false),
                actionOnDestroy: vfx => Destroy(vfx.gameObject),
                defaultCapacity: 10,
                maxSize: 20
            );
        }

        public void CastAoEBasicAttack()
        {
            if (Time.time < _nextAttackTime) return;
            _nextAttackTime = Time.time + attackCooldown;

            int numHits = Physics.OverlapSphereNonAlloc(transform.position, _playerStats.AttackRadius.GetValue(), _hitColliders, enemyLayer);

            for (int i = 0; i < numHits; i++)
            {
                Collider enemyCollider = _hitColliders[i];

                if (enemyCollider.TryGetComponent(out IDamageable enemy))
                {
                    enemy.TakeDamage(_playerStats.Damage.GetValue(), transform);
                }

                SpawnVFX(enemyCollider.transform);
            }
        }

        private void SpawnVFX(Transform enemyTransform)
        {
            SpellVFX vfx = _vfxPool.Get();

            Vector3 offset = Vector3.up * vfxSpawnHeight;
            vfx.PlayEffect(enemyTransform, offset);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 5);
        }
    }
}