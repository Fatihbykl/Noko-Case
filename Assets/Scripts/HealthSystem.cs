using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    public event Action<float> OnHealthChanged;
    public event Action OnDied;

    private float _currentHealth;
    private float _maxHealth;

    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, Transform attacker)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= amount;
        
        OnHealthChanged?.Invoke(_currentHealth / _maxHealth);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDied?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth / _maxHealth);
    }
}
