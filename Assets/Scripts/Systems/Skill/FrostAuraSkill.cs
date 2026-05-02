using Enemy.Core;
using UnityEngine;

namespace Systems.Skill
{
    public class FrostAuraSkill : BaseWeaponSkill
    {
        public float AuraRadius = 4f;
        public LayerMask EnemyLayer;
        public GameObject AuraVFX;

        private void Start()
        {
            AuraVFX.SetActive(true);
            Cooldown = 0.5f;
        }

        public override void Tick(bool hasEnemyInRange)
        {
            if (AuraVFX.activeSelf != hasEnemyInRange)
            {
                AuraVFX.SetActive(hasEnemyInRange);
            }

            base.Tick(hasEnemyInRange);
        }

        public override void ExecuteSkill()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, AuraRadius, EnemyLayer);
            foreach (var enemy in hits)
            {
                if (enemy.TryGetComponent(out EnemyController controller))
                {
                    controller.ApplySlow(0.50f, 0.6f);
                }
            }
        }
    }
}
