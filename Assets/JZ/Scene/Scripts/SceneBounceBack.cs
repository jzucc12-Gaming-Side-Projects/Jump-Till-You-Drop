using System.Collections;
using UnityEngine;

namespace JZ.SCENE
{
    public class SceneBounceBack : MonoBehaviour
    {
        [SerializeField] private float waitTimerInSeconds = 60f;
        [SerializeField] private string targetScene = null;
        [SerializeField] private AnimType animationType = AnimType.longFade;

        private void Awake() 
        {
            StartCoroutine(WaitToBouncBack());
        }

        private IEnumerator WaitToBouncBack()
        {
            float currTimer = 0;
            yield return new WaitUntil(() =>
            {
                if(JZ.INPUT.Utils.AnyKeyOrButton())
                    currTimer = 0;
                else
                    currTimer += Time.deltaTime;

                return currTimer >= waitTimerInSeconds;
            });

            FindObjectOfType<SceneTransitioner>().TransitionToScene(targetScene, animationType);
        }
    }
}
