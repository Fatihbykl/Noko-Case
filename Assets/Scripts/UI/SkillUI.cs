using Systems.Skill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillUI : MonoBehaviour
    {
        [Header("Skill Reference")]
        [SerializeField] private BaseWeaponSkill _trackedSkill; 

        [Header("UI Elements")]
        [SerializeField] private Image _cooldownOverlay;
        [SerializeField] private TextMeshProUGUI _cooldownText;

        private void Update()
        {
            if (_trackedSkill == null || _trackedSkill.IsPassive)
            {
                _cooldownOverlay.fillAmount = 0f;
                _cooldownText.text = "";
                return;
            }

            float remainingTime = _trackedSkill.GetRemainingTime();

            if (remainingTime > 0.1f)
            {
                _cooldownOverlay.fillAmount = _trackedSkill.GetCooldownPercentage();

                _cooldownText.text = Mathf.CeilToInt(remainingTime).ToString();
            }
            else
            {
                _cooldownOverlay.fillAmount = 0f;
                _cooldownText.text = "";
            }
        }
    }
}
