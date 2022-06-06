using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.CORE
{
    public class Blinker : MonoBehaviour
    {
        [SerializeField] private float blinkRate = 1f;
        [SerializeField] private float fadeTimer = 0.1f;
        private Graphic target = null;
        private bool isFading = false;
        private float currTimer = 0;


        private void Awake() 
        {
            target = GetComponent<Graphic>();
        }

        private void FixedUpdate()
        {
            if(currTimer >= blinkRate)
            {
                currTimer = 0;
                StartCoroutine(Fade());
            }

            if(isFading) return;
            currTimer += Time.deltaTime;
        }

        private IEnumerator Fade()
        {
            isFading = true;
            Color currColor = target.color;
            float startAlpha = currColor.a;
            float targetAlpha = startAlpha == 0 ? 1 : 0;
            float currAlpha = currColor.a;

            float currFadeTimer = 0;
            while(currFadeTimer < fadeTimer)
            {
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, Mathf.Min(currFadeTimer/fadeTimer,1));
                currColor.a = newAlpha;
                target.color = currColor;
                currFadeTimer += Time.deltaTime;
                yield return null;
            }

            currColor.a = targetAlpha;
            target.color = currColor;
            isFading = false;
        }
    }
}