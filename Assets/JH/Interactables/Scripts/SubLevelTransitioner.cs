using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using JH.LEVEL;
using JH.PLAYER;

namespace JH.INTERACTABLES
{
    public class SubLevelTransitioner : MonoBehaviour
    {
        #region //Transition variables
        [SerializeField] private DirectionEnum myBoundary = DirectionEnum.right;
        [SerializeField] private string targetScene = "";
        [SerializeField] private string[] scenesToUnloadWhenTriggered = new string[0];
        #endregion


        #region //Monobehaviour
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(!(other is CircleCollider2D)) return;
            if(other.GetComponent<PlayerHealth>() == null) return;

            UnloadScenes();
            FindObjectOfType<ActiveLevelManager>().OnSubLevelTransition(targetScene);
        }
        #endregion

        #region //Getters
        public bool IsLeftBoundary() { return myBoundary == DirectionEnum.left; }
        public bool IsRightBoundary() { return myBoundary == DirectionEnum.right; }
        public bool IsHorizontalBoundary() { return IsLeftBoundary() || IsRightBoundary(); }
        public bool IsTopBoundary() { return myBoundary == DirectionEnum.top; }
        public bool IsBottomBoundary() { return myBoundary == DirectionEnum.bottom; }
        public bool IsLowerBoundary() { return IsLeftBoundary() || IsBottomBoundary(); }

        #endregion

        #region //Transitioning
        private void UnloadScenes()
        {
            foreach(var scene in scenesToUnloadWhenTriggered)
            {
                if(!SceneManager.GetSceneByName(scene).isLoaded) continue;
                SceneManager.UnloadSceneAsync(scene);
            }
        }
        #endregion
    }
}