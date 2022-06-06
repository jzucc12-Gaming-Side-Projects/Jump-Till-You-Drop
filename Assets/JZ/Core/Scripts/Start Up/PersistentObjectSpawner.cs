using UnityEngine;

namespace JZ.CORE.STARTUP
{
    //Originally from GameDev.TV//
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObjectPrefab = null;
        private static bool spawned = false;

        private void Awake()
        {
            if(spawned) return;
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
            spawned = true;
        }
    }
}
