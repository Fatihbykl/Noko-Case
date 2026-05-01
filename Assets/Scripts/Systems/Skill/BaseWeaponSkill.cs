using UnityEngine;

namespace Systems.Skill
{
    public abstract class BaseWeaponSkill : MonoBehaviour
    {
        [Header("Skill Settings")]
        public string SkillName;
        public float Cooldown;
        protected float NextCastTime;

        public abstract void ExecuteSkill();

        public virtual void Tick()
        {
            if (Time.time >= NextCastTime)
            {
                ExecuteSkill();
                NextCastTime = Time.time + Cooldown;
            }
        }
    }
}
