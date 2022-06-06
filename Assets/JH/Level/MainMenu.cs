using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JH.LEVEL
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Canvas canvas = null;
        private IEnumerator Start()
        {
            if(!SceneManager.GetSceneByName("Core").isLoaded)
            {
                yield return SceneManager.LoadSceneAsync("Core", LoadSceneMode.Additive);
            }

            GetComponent<SubLevel>().enabled = true;
            GetComponent<SubLevel>().EnteredThisSubLevel();
            canvas.enabled = true;
        }
    }

}