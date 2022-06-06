using System.Collections.Generic;
using UnityEngine;

namespace JH.LEVEL
{
    public class ActiveLevelInfo : MonoBehaviour
    {
        [SerializeField] private string rootScene = "";
        [SerializeField] private Vector2 spawnPoint = Vector2.zero;
        private static List<string> beatenLevels = new List<string>();


        #region //Getters
        public string GetRootScene()
        {
            return rootScene;
        }
        public Vector2 GetSpawnPoint()
        {
            return spawnPoint;
        }
        public void SetBeatLevel(string _sceneName)
        {
            string levelNumber = GetLevelNumber(_sceneName);
            beatenLevels.Add(levelNumber);
        }
        #endregion

        #region //Setters
        public void SetRootScene(string _rootScene)
        {
            rootScene = _rootScene;
        }
        public void SetSpawnPoint(Vector2 _spawnPoint)
        {
            spawnPoint = _spawnPoint;
        }
        public bool GetBeatLevel(string _sceneName)
        {
            string levelNumber = GetLevelNumber(_sceneName);
            return beatenLevels.Contains(levelNumber);
        }
        #endregion

        private string GetLevelNumber(string _sceneName)
        {
            char firstCharacter = _sceneName.ToCharArray()[0];
            return firstCharacter.ToString();
        }
    }
}
