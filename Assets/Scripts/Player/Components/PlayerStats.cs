using Systems.Combat;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Core Stats")]
    public CharacterStat MaxHealth;
    public CharacterStat Damage;
    public CharacterStat Armor;
    public CharacterStat MoveSpeed;

    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        // Sağlık sistemine final can değerimizi veriyoruz
        _healthSystem.Initialize(MaxHealth.GetValue());
    }

    // Oyun içinden bir "Can Yükseltme" (Upgrade) yeteneği alındığında çağrılır
    public void UpgradeMaxHealth(float bonusAmount)
    {
        // Kalıcı upgrade ise BaseValue'yu artırabilirsin
        MaxHealth.BaseValue += bonusAmount;
        
        // Veya bir yüzük takıldıysa AddModifier kullanırsın
        // MaxHealth.AddModifier(bonusAmount);
        
        // Sağlık sistemini yeni max canla güncelle
        _healthSystem.Initialize(MaxHealth.GetValue()); 
    }
}
