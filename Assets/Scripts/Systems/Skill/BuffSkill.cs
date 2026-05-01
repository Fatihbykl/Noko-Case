using System.Collections;
using Player.Components;
using UnityEngine;

namespace Systems.Skill
{
    public class BuffSkill : BaseWeaponSkill
    {
        public float BuffDuration = 5f;
        public float BonusDamage = 20f;
        public float BonusAttackSpeed = 1f;
        public GameObject GlowVFX;

        private PlayerStats _playerStats;

        private void Awake()
        {
            _playerStats = GetComponentInParent<PlayerStats>();
        }

        public override void ExecuteSkill()
        {
            StartCoroutine(BuffCoroutine());
        }

        private IEnumerator BuffCoroutine()
        {
            GlowVFX.SetActive(true);

            // Statlara bonusu ekle
            _playerStats.Damage.AddModifier(BonusDamage);
            _playerStats.AttackSpeed.AddModifier(BonusAttackSpeed);

            yield return new WaitForSeconds(BuffDuration);

            // Süre bitince bonusları geri al
            _playerStats.Damage.RemoveModifier(BonusDamage);
            _playerStats.AttackSpeed.RemoveModifier(BonusAttackSpeed);
        
            GlowVFX.SetActive(false);
        }
    }
}
