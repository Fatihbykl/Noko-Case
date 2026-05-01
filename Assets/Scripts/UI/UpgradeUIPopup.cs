using UnityEngine;

namespace UI
{
    public class UpgradeUIPopup : MonoBehaviour
    {
        [SerializeField] private Canvas popupCanvas;

        private void Start()
        {
            popupCanvas.enabled = false;
        }

        public void OpenCanvas()
        {
            popupCanvas.enabled = true;
        }

        public void CloseCanvas()
        {
            popupCanvas.enabled = false;
        }
    }
}
