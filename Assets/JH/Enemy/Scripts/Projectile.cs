using System.Collections;
using JH.PLAYER;
using JH.LEVEL;
using JZ.AUDIO;
using UnityEngine;

namespace JH.ENEMY
{
    public class Projectile : MonoBehaviour, ISubLevelComponentOnExit, IOnEndLevel
    {
        #region //Cached components
        [SerializeField] private Rigidbody2D rb = null;
        [SerializeField] private SpriteRenderer spriteRenderer = null;
        [SerializeField] private LayerMask bouncyLayer = -1;
        [SerializeField] private GameObject jumpPad = null;
        #endregion

        #region //Projectile variables
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float lifeSpan = 5f;
        #endregion


        #region //Monobehaviour
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(lifeSpan);
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            spriteRenderer.enabled = false;
            jumpPad.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth == null && !other.CompareTag("Ground")) return;


            if (playerHealth == null)
                DetonateProjectile(playerHealth);
            else if (!other.IsTouchingLayers(bouncyLayer))
            {
                playerHealth.KillPlayer("HIT BY A BULLET!");
                DetonateProjectile(playerHealth);
            }
        }
        #endregion


        private void DetonateProjectile(PlayerHealth playerHealth)
        {
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<SoundPlayer>().Play("Hit");
            rb.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            jumpPad.SetActive(false);
            Destroy(gameObject, 1f);
        }

        public void SetDirection(Vector2 _direction)
        {
            rb.velocity = _direction * moveSpeed;
            if(_direction == Vector2.left)
                spriteRenderer.flipX = true;
            
            if(_direction == Vector2.up)
                transform.Rotate(0, 0, 90);
            
            if(_direction == Vector2.down) 
                transform.Rotate(0, 0, -90);
        }

        public void OnExitingSubLevel()
        {
            Destroy(gameObject);
        }

        public void OnEndLevel()
        {
            GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }
}
