using JH.PLAYER;
using JZ.AUDIO;
using UnityEngine;

namespace JH.INTERACTABLES
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField] float jumpForce = 17f;
        private SoundPlayer sfxPlayer = null;
        private Animator animator = null;


        private void Awake() 
        {
            animator = GetComponent<Animator>();
            sfxPlayer = GetComponent<SoundPlayer>();    
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.GetComponent<PlayerHealth>() == null) return;
            animator.SetTrigger("Jump");
            sfxPlayer.Play("Jump Pad");
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}