using System;
using Player.Core;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using VFX;

namespace Player.Components
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private GameObject rangeSprite;
        
        [Header("VFX Settings")] 
        [SerializeField] private SpellVFX spellPrefab;
        [SerializeField] private float vfxSpawnHeight = 3f;

        private float _nextAttackTime;
        private Collider[] _hitColliders = new Collider[20];
        private PlayerStats _playerStats;
        private PlayerController _player;
        private ObjectPool<SpellVFX> _vfxPool;

        private void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
            _player = GetComponent<PlayerController>();
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

            var radius = _playerStats.AttackRadius.GetValue() / 2f;
            rangeSprite.transform.localScale = new Vector3(radius, radius, radius);
            rangeSprite.SetActive(false);
        }

        private void Update()
        {
            if (Time.time < _nextAttackTime) return;

            AutoAttackScanner();
        }

        private void AutoAttackScanner()
        {
            int numHits = Physics.OverlapSphereNonAlloc(transform.position, _playerStats.AttackRadius.GetValue(), _hitColliders, enemyLayer);

            if (numHits > 0)
            {
                rangeSprite.SetActive(true);
                CastAoEBasicAttack(numHits);

                float currentAttackSpeed = _playerStats.AttackSpeed.GetValue();
                Debug.Log("current attack speed - " + currentAttackSpeed);
                if (currentAttackSpeed <= 0.1f) currentAttackSpeed = 0.1f;
            
                float cooldown = 1f / currentAttackSpeed;
                _nextAttackTime = Time.time + cooldown;
            }
            else
            {
                rangeSprite.SetActive(false);
            }
        }

        private void CastAoEBasicAttack(int numHits)
        {
            _player.Animator.SetTrigger(AnimHashes.AttackTrigger);

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
            Gizmos.DrawWireSphere(transform.position, 3);
        }
    }
}