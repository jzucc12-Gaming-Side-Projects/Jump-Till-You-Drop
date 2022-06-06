using UnityEngine;
using JH.PLAYER;

namespace JH.INTERACTABLES
{
    public class DeathPit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(!(other is CircleCollider2D)) return;
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if(playerHealth == null) return;

            playerHealth.KillPlayer("FELL DOWN A PIT!");
        }
    }
}
