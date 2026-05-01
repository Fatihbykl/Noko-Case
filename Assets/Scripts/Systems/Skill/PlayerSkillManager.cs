using UnityEngine;

namespace Systems.Skill
{
    public class PlayerSkillManager : MonoBehaviour
    {
        private BaseWeaponSkill[] _equippedSkills;

        private void Awake()
        {
            _equippedSkills = GetComponentsInChildren<BaseWeaponSkill>();
        }

        private void Update()
        {
            foreach (var skill in _equippedSkills)
            {
                skill.Tick();
            }
        }
    }
}
