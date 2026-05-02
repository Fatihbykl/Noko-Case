using UnityEngine;

namespace Systems.Skill
{
    public class PlayerSkillManager : MonoBehaviour
    {
        [SerializeField] private float activationRadius = 3f;
        [SerializeField] private LayerMask enemyLayer;
        
        private BaseWeaponSkill[] _equippedSkills;
        private Collider[] _radarHits = new Collider[1];

        private void Awake()
        {
            _equippedSkills = GetComponentsInChildren<BaseWeaponSkill>();
        }

        private void Update()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, activationRadius, _radarHits, enemyLayer);
            bool hasEnemy = hitCount > 0;
            
            foreach (var skill in _equippedSkills)
            {
                skill.Tick(hasEnemy);
            }
        }
    }
}
