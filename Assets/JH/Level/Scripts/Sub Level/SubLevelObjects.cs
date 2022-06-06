using UnityEngine;

namespace JH.LEVEL
{
    public class SubLevelObjects : MonoBehaviour, ISubLevelComponentOnEnter, ISubLevelComponentOnExit
    {
        [SerializeField] GameObject[] objectsToToggle = new GameObject[0];
        public void OnEnteringSubLevel()
        {
            foreach(GameObject go in objectsToToggle)
                go.SetActive(true);
        }

        public void OnExitingSubLevel()
        {
            foreach(GameObject go in objectsToToggle)
                go.SetActive(false);
        }
    }
}
