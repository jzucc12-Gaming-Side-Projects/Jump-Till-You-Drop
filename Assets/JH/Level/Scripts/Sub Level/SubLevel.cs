using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JH.LEVEL
{
    public class SubLevel : MonoBehaviour
    {
        #region //Sublevel properties
        [SerializeField] private string[] scenesToLoadWhenEntered = new string[0];
        public static event Action<string, SubLevel> subLevelEnabled;
        public static event Action<string> subLevelDisabled;
        #endregion

        #region //Sub-level components
        private ISubLevelComponentOnEnter[] enterComponents = new ISubLevelComponentOnEnter[0];
        private ISubLevelComponentOnExit[] exitComponents = new ISubLevelComponentOnExit[0];
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            enterComponents = GetComponentsInChildren<ISubLevelComponentOnEnter>();
            exitComponents = GetComponentsInChildren<ISubLevelComponentOnExit>();
        }

        private void OnEnable() 
        {
            subLevelEnabled?.Invoke(gameObject.scene.name, this);
        }

        private void OnDisable() 
        {
            subLevelDisabled?.Invoke(gameObject.scene.name);
        }
        #endregion

        #region //Transitions
        //Public
        public void EnteredThisSubLevel()
        {
            LoadScenesOnEnter();
            foreach(var enterComponent in enterComponents)
                enterComponent.OnEnteringSubLevel();
        }

        public void ExitedThisSubLevel()
        {
            foreach(var exitComponent in exitComponents)
                exitComponent.OnExitingSubLevel();
        }
        public void Hide()
        {
            ExitedThisSubLevel();
            foreach(var sr in GetComponentsInChildren<SpriteRenderer>())
                sr.gameObject.SetActive(false);
        }

        //Private
        private void LoadScenesOnEnter()
        {
            foreach(var scene in scenesToLoadWhenEntered)
            {
                if(SceneManager.GetSceneByName(scene).isLoaded) continue;
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }
        #endregion
    }
}
