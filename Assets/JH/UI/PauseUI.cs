using UnityEngine;
using JH.PLAYER;
using JH.LEVEL;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

namespace JH.UI
{
    public class PauseUI : MonoBehaviour, IOnResetLevel, IOnEndLevel
    {
        [SerializeField] private TextMeshProUGUI screenText = null;
        [SerializeField] private GameObject pausePanel = null;
        [SerializeField] private GameObject restartButton = null;
        [SerializeField] private PlayerHealth playerHealth = null;
        private bool ended = false;


        private void Start()
        {
            SetText("Game Paused");
        }

        private void OnEnable()
        {
            playerHealth.OnPlayerDied += SetText;
        }

        private void OnDisable()
        {
            playerHealth.OnPlayerDied -= SetText;
        }

        private void Update()
        {
            if(ended) return;
            if(!Input.GetKeyDown(KeyCode.Escape)) return;

            if(pausePanel.activeInHierarchy)
                DeactivatePanel();
            else
                ActivatePanel();
        }

        public void OnResetLevel()
        {
            ended = false;
            SetText("Game Paused");
            DeactivatePanel();
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void OnEndLevel()
        {
            ended = true;
            ActivatePanel();
        }

        #region //Activation
        private void ActivatePanel()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            bool inMenu = SceneManager.GetSceneByName("Level Menu").isLoaded;
            restartButton.SetActive(!inMenu);
        }

        private void DeactivatePanel()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

        private void SetText(string _text)
        {
            screenText.text = _text;
        }
        #endregion
    }
}
