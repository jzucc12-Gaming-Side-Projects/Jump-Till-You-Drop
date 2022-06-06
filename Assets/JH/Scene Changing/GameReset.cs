using UnityEngine;

namespace JH.LEVEL
{
    public class GameReset : MonoBehaviour
    {
        private ActiveLevelInfo levelInfo = null;
        private LevelInterfaceCaller interfaceCaller = null;
        [SerializeField] Vector2 mainMenuSpawnPoint = Vector2.zero;


        private void Start()
        {
            levelInfo = FindObjectOfType<ActiveLevelInfo>();
            interfaceCaller = FindObjectOfType<LevelInterfaceCaller>();
        }

        private void Update()
        {
            if(!PlayerPrefKeys.InDevMode()) return;
            if(Input.GetKeyDown(KeyCode.R))
                ResetFromMainMenu();

            if(Input.GetKeyDown(KeyCode.T))
                interfaceCaller.ResetLevel();
        }

        public void ResetFromMainMenu()
        {
            Time.timeScale = 1;
            levelInfo.SetRootScene("Level Menu");
            levelInfo.SetSpawnPoint(mainMenuSpawnPoint);
            FindObjectOfType<BackgroundChooser>().HideBackground();
            interfaceCaller.ResetLevel();
        }

        public void ResetFromCurrentLevel()
        {
            interfaceCaller.ResetLevel();
        }
    }
}