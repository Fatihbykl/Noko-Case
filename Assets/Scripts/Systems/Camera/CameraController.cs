using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Setup")]
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 10, -10);

    [Header("Follow Dynamics")]
    [SerializeField, Range(0.01f, 1f)] 
    private float _smoothTime = 0.12f;

    private Vector3 _currentVelocity = Vector3.zero;

    private void Start()
    {
        if (_target != null)
        {
            transform.position = _target.position + _offset;
            transform.LookAt(_target);
        }
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPosition = _target.position + _offset;

        transform.position = Vector3.SmoothDamp(
            transform.position, 
            targetPosition, 
            ref _currentVelocity, 
            _smoothTime
        );
    }
}