using System.Collections.Generic;
using Systems.Combat;
using Systems.Stats;
using UnityEngine;

namespace Player.Components
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Core Stats")]
        [SerializeField] private CharacterStat maxHealth;
        [SerializeField] private CharacterStat damage;
        [SerializeField] private CharacterStat attackSpeed;
        [SerializeField] private CharacterStat moveSpeed;
        [SerializeField] private CharacterStat attackRadius;

        public CharacterStat MaxHealth => maxHealth;
        public CharacterStat Damage => damage;
        public CharacterStat AttackSpeed => attackSpeed;
        public CharacterStat MoveSpeed => moveSpeed;
        public CharacterStat AttackRadius => attackRadius;

        private HealthSystem _healthSystem;
    
        private Dictionary<StatType, CharacterStat> _statDictionary;

        private void Awake()
        {
            _healthSystem = GetComponent<HealthSystem>();

            _statDictionary = new Dictionary<StatType, CharacterStat>
            {
                { StatType.MaxHealth, maxHealth },
                { StatType.Damage, damage },
                { StatType.AttackSpeed, attackSpeed },
                { StatType.MoveSpeed, moveSpeed }
            };
        }

        private void Start()
        {
            _healthSystem.Initialize(maxHealth.GetValue());
        }

        public void UpgradeStat(StatType statType, float amount)
        {
            if (_statDictionary.TryGetValue(statType, out CharacterStat statToUpgrade))
            {
                statToUpgrade.BaseValue += amount;

                if (statType == StatType.MaxHealth)
                {
                    _healthSystem.Initialize(maxHealth.GetValue());
                }

                Debug.Log($"{statType} yükseltildi! Yeni taban değer: {statToUpgrade.BaseValue}");
            }
            else
            {
                Debug.LogWarning($"Stat tipi bulunamadı: {statType}");
            }
        }
    }
}
