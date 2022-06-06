using System.Collections;
using JH.LEVEL;
using JH.PLAYER;
using JZ.AUDIO;
using UnityEngine;

namespace JH.INTERACTABLES
{
    public class EndLevelMarker : MonoBehaviour
    {
        private float endDelay = 2f;
        private float slowDownScale = 0.1f;
        private bool hit = false;

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(hit) return;
            if (other.GetComponent<PlayerHealth>() == null) return;
            FindObjectOfType<ActiveLevelInfo>().SetBeatLevel(gameObject.scene.name);
            GetComponent<SoundPlayer>().Play("Level End");
            StartCoroutine(EndLevel());
            hit = true;
        }

        private IEnumerator EndLevel()
        {
            Time.timeScale = slowDownScale;
            GetComponent<Animator>().SetTrigger("Shrink");
            yield return new WaitForSeconds(endDelay * slowDownScale);
            Time.timeScale = 1;
            FindObjectOfType<GameReset>().ResetFromMainMenu();
        }
    }
}