using Systems.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeButtonUI : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private StatType myStatType;
    
        [Header("References")]
        [SerializeField] private StatUpgradeManager upgradeManager;
        [SerializeField] private TextMeshProUGUI costText;
    
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnUpgradeButtonClicked);
        }

        private void OnEnable()
        {
            RefreshCostText();
        }

        private void OnUpgradeButtonClicked()
        {
            upgradeManager.BuyUpgrade(myStatType);
        
            RefreshCostText();
        }

        public void RefreshCostText()
        {
            int currentCost = upgradeManager.GetCurrentCost(myStatType);
        
            if (currentCost == -1)
            {
                costText.text = "MAX";
                _button.interactable = false;
            }
            else
            {
                costText.text = currentCost.ToString() + " G";
            }
        }
    }
}
