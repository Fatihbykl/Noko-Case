using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Header("References")]
        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _handle;

        [Header("Settings")]
        [SerializeField] private float _joystickRadius = 100f;

        public Vector2 InputVector { get; private set; } 

        private void Start()
        {
            _background.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _background.gameObject.SetActive(true);
            _background.position = eventData.position;
        
            _handle.anchoredPosition = Vector2.zero;
            InputVector = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - (Vector2)_background.position;
        
            InputVector = Vector2.ClampMagnitude(direction / _joystickRadius, 1f);
        
            _handle.anchoredPosition = InputVector * _joystickRadius;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _background.gameObject.SetActive(false);
            InputVector = Vector2.zero;
            _handle.anchoredPosition = Vector2.zero;
        }
    }
}