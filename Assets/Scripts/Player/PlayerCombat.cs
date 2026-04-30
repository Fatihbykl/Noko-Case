using UnityEngine;
using UnityEngine.Pool;
using VFX;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("AoE Attack Settings")]
    [SerializeField] private float _attackRadius = 5f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackCooldown = 1f;
    private float _nextAttackTime;

    [Header("VFX Settings")]
    [SerializeField] private SpellVFX _spellPrefab;
    [SerializeField] private float _vfxSpawnHeight = 3f;

    private Collider[] _hitColliders = new Collider[20]; 
    
    private ObjectPool<SpellVFX> _vfxPool;

    private void Start()
    {
        _vfxPool = new ObjectPool<SpellVFX>(
            createFunc: () => {
                var vfx = Instantiate(_spellPrefab);
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
        _nextAttackTime = Time.time + _attackCooldown;

        int numHits = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _hitColliders, _enemyLayer);

        for (int i = 0; i < numHits; i++)
        {
            Collider enemyCollider = _hitColliders[i];
            
            if (enemyCollider.TryGetComponent(out EnemyController enemy))
            {
                enemy.TakeDamage(_damage, transform);
            }

            SpawnVFX(enemyCollider.transform);
        }
    }

    private void SpawnVFX(Transform enemyTransform)
    {
        SpellVFX vfx = _vfxPool.Get();
        
        Vector3 offset = Vector3.up * _vfxSpawnHeight;
        vfx.PlayEffect(enemyTransform, offset);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
    }
}
