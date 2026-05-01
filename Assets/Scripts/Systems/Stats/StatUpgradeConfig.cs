using System;

namespace Systems.Stats
{
    [Serializable]
    public class StatUpgradeConfig
    {
        public StatType StatType;
        public int BaseCost = 50;
        public int CostIncrement = 25;
        public float UpgradeAmount = 5f;
        public int MaxLevel = 10;
    }
}