using UnityEngine;

namespace Systems.Skill
{
    public abstract class BaseWeaponSkill : MonoBehaviour
    {
        [Header("Skill Settings")]
        public string SkillName;
        public float Cooldown;
        public bool IsPassive;
        protected float NextCastTime;

        public abstract void ExecuteSkill();

        public virtual void Tick(bool hasEnemyInRange)
        {
            if (Time.time >= NextCastTime && hasEnemyInRange)
            {
                ExecuteSkill();
                NextCastTime = Time.time + Cooldown;
            }
        }
        
        public float GetCooldownPercentage()
        {
            if (IsPassive || Cooldown <= 0) return 0f;
        
            float remaining = NextCastTime - Time.time;
            
            return Mathf.Clamp01(remaining / Cooldown); 
        }

        public float GetRemainingTime()
        {
            if (IsPassive) return 0f;
            return Mathf.Max(0, NextCastTime - Time.time);
        }
    }
}
