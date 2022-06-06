using JH.PLAYER;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JH.UI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth = null;
        private HealthImage[] healthImages = null;
        private TextMeshProUGUI healthDisplay = null;


        #region //Monobehaviour
        private void Awake()
        {
            healthDisplay = GetComponent<TextMeshProUGUI>();
            healthImages = GetComponentsInChildren<HealthImage>();
        }

        private void OnEnable()
        {
            playerHealth.OnPlayerHealthChange += UpdateUI;
            playerHealth.AddedNewHealth += HealthGrow;
        }

        private void OnDisable()
        {
            playerHealth.OnPlayerHealthChange -= UpdateUI;
            playerHealth.AddedNewHealth += HealthGrow;
        }
        #endregion

        private void UpdateUI(int _currentHealth)
        {
            for(int ii = 0; ii < healthImages.Length; ii++)
            {
                if(ii < _currentHealth)
                {
                    healthImages[ii].ChangeImageState(true);
                }
                else
                {
                    healthImages[ii].ChangeImageState(false);
                }
            }
        }

        private void HealthGrow(int _newMaxHealth)
        {
            int index = _newMaxHealth - 1;
            var target = healthImages[index];
            target.enabled = true;
            target.Grow();
        }
    }
}
