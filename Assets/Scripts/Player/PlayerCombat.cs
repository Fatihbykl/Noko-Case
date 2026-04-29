using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Transform _wandFirePoint;
        [SerializeField] private GameObject _spellPrefab;
        [SerializeField] private float _fireRate = 0.5f;
        private float _nextFireTime;

        public void CastSpell()
        {
            if (Time.time >= _nextFireTime)
            {
                _nextFireTime = Time.time + _fireRate;
                Instantiate(_spellPrefab, _wandFirePoint.position, _wandFirePoint.rotation);
                // Burada Animator tetiklenebilir: _animator.SetTrigger("CastSpell");
            }
        }
    }
}
