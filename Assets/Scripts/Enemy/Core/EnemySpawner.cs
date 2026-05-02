using UnityEngine;
using UnityEngine.Pool;

namespace Enemy.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private EnemyController _enemyPrefab;
        [SerializeField] private int _maxEnemies = 10;
        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private float _spawnRadius = 15f;

        // Unity'nin kendi Object Pool objesi
        private ObjectPool<EnemyController> _enemyPool;
    
        private int _activeEnemyCount = 0;
        private float _nextSpawnTime;

        private void Awake()
        {
            // Havuzu başlatıyoruz
            _enemyPool = new ObjectPool<EnemyController>(
                createFunc: CreateEnemy,
                actionOnGet: OnTakeEnemyFromPool,
                actionOnRelease: OnReturnEnemy,
                actionOnDestroy: OnDestroyEnemy,
                collectionCheck: false,
                defaultCapacity: _maxEnemies,
                maxSize: _maxEnemies * 2
            );
        }

        private void Update()
        {
            if (_activeEnemyCount < _maxEnemies && Time.time >= _nextSpawnTime)
            {
                SpawnEnemy();
                _nextSpawnTime = Time.time + _spawnInterval;
            }
        }

        private void SpawnEnemy()
        {
            EnemyController enemy = _enemyPool.Get();

            Vector2 randomPoint = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);

            enemy.transform.position = spawnPos;
            enemy.ResetEnemy(); 
        }
        
        private EnemyController CreateEnemy()
        {
            EnemyController enemy = Instantiate(_enemyPrefab, transform);
        
            enemy.OnDespawnRequest += ReturnEnemyToPool;
        
            return enemy;
        }

        private void OnTakeEnemyFromPool(EnemyController enemy)
        {
            enemy.gameObject.SetActive(true);
            _activeEnemyCount++;
        }

        private void OnReturnEnemy(EnemyController enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void OnDestroyEnemy(EnemyController enemy)
        {
            Destroy(enemy.gameObject);
        }

        private void ReturnEnemyToPool(EnemyController enemy)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                _activeEnemyCount--;
                _enemyPool.Release(enemy);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }
    }
}