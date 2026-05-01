using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public bool canAttack;
        public int maxHealth;
        public float chaseSpeed;
        public float fleeSpeed;
        public float patrolSpeed;
        public float patrolRadius;
        public float attackRange;
        public float damage;
        public float navMeshRecalculateDelay;
    }
}
