using JH.LEVEL;
using JH.PLAYER;
using UnityEngine;

namespace JH.INTERACTABLES
{
    public class JumpPowerUp : MonoBehaviour
    {
        private bool isActive = true;
        private PlayerHealth playerToModify = null;


        private void Start()
        {
            bool levelBeaten = FindObjectOfType<ActiveLevelInfo>().GetBeatLevel(gameObject.scene.name);
            isActive = !levelBeaten;
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(!isActive) return;
            playerToModify = other.GetComponent<PlayerHealth>();
            if(playerToModify == null) return;
            playerToModify.PlayerHeal();
            isActive = false;
        }

        public void IncreasePlayerHealth()
        {
            if(playerToModify == null) return;
            playerToModify.IncreaseMaxHealth();
        }
    }
}