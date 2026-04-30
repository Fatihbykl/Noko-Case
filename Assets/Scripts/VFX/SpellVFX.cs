using System;
using UnityEngine;
using UnityEngine.Pool;

namespace VFX
{
    public class SpellVFX : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private IObjectPool<SpellVFX> _pool;
        
        private Transform _target;
        private Vector3 _offset;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        
            var main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        public void SetPool(IObjectPool<SpellVFX> pool)
        {
            _pool = pool;
        }

        public void PlayEffect(Transform target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
        
            transform.position = _target.position + _offset;
            
            _particleSystem.Play();
        }

        private void LateUpdate()
        {
            if (_target == null)
            {
                if (_particleSystem.isPlaying)
                {
                    _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
                return;
            }

            transform.position = _target.position + _offset;
        }

        private void OnParticleSystemStopped()
        {
            _target = null;
            _pool.Release(this);
        }
    }
}
