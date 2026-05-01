using System;
using UnityEngine;

namespace Player.Components
{
    public class PlayerWallet : MonoBehaviour
    {
        public event Action<int> OnGoldChanged;

        [SerializeField] private int currentGold = 0;
        public int CurrentGold => currentGold;

        public void AddGold(int amount)
        {
            if (amount <= 0) return;
        
            currentGold += amount;
            OnGoldChanged?.Invoke(currentGold);
            Debug.Log($"Altın kazanıldı! Mevcut Altın: {currentGold}");
        }

        public bool TrySpendGold(int amount)
        {
            if (currentGold >= amount)
            {
                currentGold -= amount;
                OnGoldChanged?.Invoke(currentGold);
                return true;
            }
        
            Debug.Log("Yetersiz altın!");
            return false;
        }
    }
}