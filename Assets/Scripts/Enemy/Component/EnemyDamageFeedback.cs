using System.Collections;
using Systems.Combat;
using UnityEngine;

namespace Enemy.Component
{
    public class EnemyDamageFeedback : MonoBehaviour
    {
        [Header("Flash Settings")] [SerializeField]
        private SkinnedMeshRenderer _meshRenderer;

        [SerializeField] private Material _flashMaterial;
        [SerializeField] private float _flashDuration = 0.1f;

        [Header("VFX Settings")] [SerializeField]
        private GameObject _hitVfxPrefab;

        [SerializeField] private Transform _vfxSpawnPoint;

        private HealthSystem _healthSystem;
        private Material _originalMaterial;
        private Coroutine _flashCoroutine;

        private void Awake()
        {
            _healthSystem = GetComponent<HealthSystem>();

            if (_meshRenderer != null)
            {
                _originalMaterial = _meshRenderer.material;
            }
        }

        private void OnEnable()
        {
            _healthSystem.OnDamaged += PlayFeedback;
        }

        private void OnDisable()
        {
            _healthSystem.OnDamaged -= PlayFeedback;
        }

        private void PlayFeedback(Transform attacker)
        {
            Debug.Log("Hasar Feedback Tetiklendi!");

            if (_meshRenderer != null && _flashMaterial != null)
            {
                if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
                _flashCoroutine = StartCoroutine(FlashRoutine());
            }

            if (_hitVfxPrefab != null)
            {
                Vector3 spawnPos = _vfxSpawnPoint != null ? _vfxSpawnPoint.position : transform.position;
                Vector3 lookDir = attacker != null ? (attacker.position - transform.position).normalized : Vector3.up;
                Instantiate(_hitVfxPrefab, spawnPos, Quaternion.LookRotation(lookDir));
            }
        }

        private IEnumerator FlashRoutine()
        {
            _meshRenderer.material = _flashMaterial;

            yield return new WaitForSeconds(_flashDuration);

            _meshRenderer.material = _originalMaterial;
        }
    }
}