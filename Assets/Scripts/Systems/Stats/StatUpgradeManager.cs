using System.Collections.Generic;
using Player.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.Stats
{
    public class StatUpgradeManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerWallet playerWallet;
        [SerializeField] private PlayerStats playerStats;

        [Header("Configurations")]
        [SerializeField] private List<StatUpgradeConfig> _upgradeConfigs;

        private Dictionary<StatType, StatUpgradeConfig> _configDict;
        private Dictionary<StatType, int> _statLevels;

        private void Awake()
        {
            _configDict = new Dictionary<StatType, StatUpgradeConfig>();
            _statLevels = new Dictionary<StatType, int>();

            foreach (var config in _upgradeConfigs)
            {
                if (_configDict.TryAdd(config.StatType, config))
                {
                    _statLevels.Add(config.StatType, 0);
                }
            }
        }

        public int GetCurrentCost(StatType type)
        {
            if (_configDict.TryGetValue(type, out var config) && _statLevels.TryGetValue(type, out int currentLevel))
            {
                // Formül: Taban Fiyat + (Mevcut Seviye * Artış Miktarı)
                // Örn: 50 + (0 * 25) = 50. Sonraki: 50 + (1 * 25) = 75 vb.
                return config.BaseCost + (currentLevel * config.CostIncrement);
            }
            return -1;
        }

        public void BuyUpgrade(StatType type)
        {
            if (!_configDict.TryGetValue(type, out var config)) return;

            int currentLevel = _statLevels[type];

            if (currentLevel >= config.MaxLevel)
            {
                Debug.Log($"{type} zaten maksimum seviyede!");
                return;
            }

            int currentCost = GetCurrentCost(type);

            if (playerWallet.TrySpendGold(currentCost))
            {
                playerStats.UpgradeStat(type, config.UpgradeAmount);
            
                _statLevels[type]++;
            
                Debug.Log($"{type} yükseltildi! Yeni Seviye: {_statLevels[type]}, Bir Sonraki Fiyat: {GetCurrentCost(type)}");
            }
        }
    }
}
