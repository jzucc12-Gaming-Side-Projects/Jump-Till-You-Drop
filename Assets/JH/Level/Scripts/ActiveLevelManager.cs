using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JH.LEVEL
{
    public class ActiveLevelManager : MonoBehaviour, IOnStartLevel
    {
        private Dictionary<string, SubLevel> loadedSubLevels = new Dictionary<string, SubLevel>();
        private SubLevel activeSubLevel = null;
        private ActiveLevelInfo levelInfo = null;
        private BackgroundChooser bgChooser = null;
        public static event Action FinishedLoading;


        #region //Monobehaviour
        private void Awake()
        {
            levelInfo = GetComponent<ActiveLevelInfo>();
            bgChooser = FindObjectOfType<BackgroundChooser>();
        }

        private void OnEnable() 
        {
            SubLevel.subLevelEnabled += AddSubLevelToDictionary;
            SubLevel.subLevelDisabled += RemoveSubLevelFromDictionary;
        }

        private void OnDisable() 
        {
            SubLevel.subLevelEnabled -= AddSubLevelToDictionary;
            SubLevel.subLevelDisabled -= RemoveSubLevelFromDictionary;
        }

        private void Start()
        {
            var subLevel = FindObjectOfType<SubLevel>();
            if(subLevel != null)
            {
                AddSubLevelToDictionary(subLevel.gameObject.scene.name, subLevel);
                subLevel.EnteredThisSubLevel();
            }
        }
        #endregion

        #region //Level Starting
        //Public
        public void OnStartLevel()
        {
            StartCoroutine(StartLevelRoutine(levelInfo.GetRootScene()));
        }

        //Private
        private IEnumerator StartLevelRoutine(string _sceneName)
        {
            Dictionary<string, SubLevel> copy;
            copy = new Dictionary<string, SubLevel>(loadedSubLevels);

            HideSubLevels(_sceneName, copy);
            ResetActiveSubLevels(copy.Values);
            yield return SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            UnloadLoadedSubLevelScenes(copy.Keys);
            OnSubLevelTransition(_sceneName);
            bgChooser.ChooseBackground(_sceneName);
            FinishedLoading?.Invoke();
        }

        private void HideSubLevels(string _newRoot, Dictionary<string, SubLevel> _copy)
        {
            foreach(string subLevelName in _copy.Keys)
            {
                if(_newRoot == subLevelName) continue;
                _copy[subLevelName].Hide();
            }
        }

        private void ResetActiveSubLevels(IEnumerable<SubLevel> _subLevels)
        {
            foreach (SubLevel subLevel in _subLevels)
            {
                subLevel.enabled = false;
            }
            
            loadedSubLevels = new Dictionary<string, SubLevel>();
            activeSubLevel = null;
        }

        private void UnloadLoadedSubLevelScenes(IEnumerable<string> _subLevelNames)
        {
            foreach(string subLevelName in _subLevelNames)
            {  
                SceneManager.UnloadSceneAsync(subLevelName);
            }
        }
        #endregion

        #region //Sub-level transitioning
        //Public
        public void OnSubLevelTransition(string _targetSceneName)
        {
            if(activeSubLevel != null)
                activeSubLevel.ExitedThisSubLevel();

            if(loadedSubLevels.ContainsKey(_targetSceneName))
            {
                SubLevel targetSubLevel = loadedSubLevels[_targetSceneName];
                activeSubLevel = targetSubLevel;
                activeSubLevel.EnteredThisSubLevel();
            }
        }

        //Private
        private void AddSubLevelToDictionary(string _sceneName, SubLevel _subLevel)
        {
            if(loadedSubLevels.ContainsValue(_subLevel)) return;
            loadedSubLevels.Add(_sceneName, _subLevel);
            _subLevel.ExitedThisSubLevel();
        }

        private void RemoveSubLevelFromDictionary(string _sceneName)
        {
            loadedSubLevels.Remove(_sceneName);
        }
        #endregion
    }
}