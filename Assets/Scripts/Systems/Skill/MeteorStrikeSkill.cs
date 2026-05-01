using UnityEngine;

namespace Systems.Skill
{
    public class MeteorStrikeSkill : BaseWeaponSkill
    {
        public float Damage = 50f;
        public float ExplosionRadius = 3f;
        public LayerMask EnemyLayer;
        public GameObject MeteorVFX;

        public override void ExecuteSkill()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 10f, EnemyLayer);
            if (hits.Length == 0) return;
        
            Transform target = hits[0].transform;

            Instantiate(MeteorVFX, target.position, Quaternion.identity);

            Collider[] splashHits = Physics.OverlapSphere(target.position, ExplosionRadius, EnemyLayer);
            foreach (var enemy in splashHits)
            {
                if (enemy.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(Damage, transform);
                    // NOT: Düşmana ApplyStun() gibi bir metot eklenebilir
                }
            }
        }
    }
}
