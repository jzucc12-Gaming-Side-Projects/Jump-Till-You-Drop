using JH.LEVEL;
using JH.PLAYER;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JH.INTERACTABLES
{
    public class StartLevelButton : MonoBehaviour
    {
        [SerializeField] private string targetScene = null;


        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(!(other is CircleCollider2D)) return;
            if(other.GetComponent<PlayerHealth>() == null) return;
            FindObjectOfType<ActiveLevelInfo>().SetRootScene(targetScene);
            FindObjectOfType<ActiveLevelInfo>().SetSpawnPoint(other.transform.position);
            FindObjectOfType<LevelInterfaceCaller>().StartLevel();
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}