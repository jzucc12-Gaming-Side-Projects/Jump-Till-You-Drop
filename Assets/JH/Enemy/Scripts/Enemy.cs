using JH.PLAYER;
using UnityEngine;

namespace JH.ENEMY
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private LayerMask bounceLayer = -1;
        [SerializeField] private GameObject bouncyObject = null;
        [SerializeField] private bool isBouncy = false;


        private void Start()
        {
            bouncyObject.SetActive(isBouncy);
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.IsTouchingLayers(bounceLayer)) return;
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if(playerHealth == null) return;
            playerHealth.KillPlayer("SLAPPED BY A BADDIE!");
        }
    }
}
