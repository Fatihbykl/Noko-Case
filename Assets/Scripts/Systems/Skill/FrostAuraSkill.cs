using Enemy.Core;
using UnityEngine;

namespace Systems.Skill
{
    public class FrostAuraSkill : BaseWeaponSkill
    {
        public float AuraRadius = 4f;
        public LayerMask EnemyLayer;
        public GameObject AuraVFX; // Karakterin altına eklenecek buz çemberi

        private void Start()
        {
            // Pasif olduğu için görseli baştan açıyoruz
            AuraVFX.SetActive(true);
            // Pasif yeteneklerin Cooldown'u yoktur, Tick'i farklı çalışır
            Cooldown = 0.5f; // Saniyede 2 kere tarama yapması yeterli (Optimizasyon)
        }

        public override void ExecuteSkill()
        {
            // Bu alana giren düşmanların NavMeshAgent hızını geçici olarak düşüren mantık
            Collider[] hits = Physics.OverlapSphere(transform.position, AuraRadius, EnemyLayer);
            foreach (var enemy in hits)
            {
                if (enemy.TryGetComponent(out EnemyController controller))
                {
                    // Düşmanın içinde bir "ApplySlow(float amount, float duration)" metodu olduğunu varsayıyoruz
                    // controller.ApplySlow(0.5f, 1f); 
                }
            }
        }
    }
}
