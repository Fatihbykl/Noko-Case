using Systems.Combat;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private HealthSystem playerHealth;

        private void OnEnable()
        {
            playerHealth.OnHealthChanged += UpdateHealthBar;
        }

        private void OnDisable()
        {
            playerHealth.OnHealthChanged -= UpdateHealthBar;
        }

        private void Start()
        {
            healthText.text = ((int)playerHealth.MaxHealth).ToString();
        }

        private void UpdateHealthBar(float healthPercentage)
        {
            healthText.text = ((int)playerHealth.MaxHealth).ToString();
        }
    }
}
