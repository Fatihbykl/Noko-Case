using Player.Components;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GoldUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private PlayerWallet playerWallet;

        private void OnEnable()
        {
            playerWallet.OnGoldChanged += UpdateGoldText;
        }

        private void OnDisable()
        {
            playerWallet.OnGoldChanged -= UpdateGoldText;
        }

        private void Start()
        {
            UpdateGoldText(playerWallet.CurrentGold);
        }

        private void UpdateGoldText(int currentGold)
        {
            goldText.text = currentGold.ToString();
        }
    }
}
