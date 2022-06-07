using System;
using JH.LEVEL;
using UnityEngine;

namespace JH.PLAYER
{
    public class PlayerMovement : MonoBehaviour, IOnResetLevel
    {
        #region //Cached components
        private Rigidbody2D rb = null;
        private ActiveLevelInfo levelInfo = null;
        private Animator animator = null;
        #endregion

        #region //Movement variables
        [SerializeField] private int movementSpeed = 5;
        [SerializeField] private float jumpForce = 20f;
        private bool jumped = false;
        private float xMove = 0;
        public event Action onJumped;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            rb = GetComponent<Rigidbody2D>();
            levelInfo = FindObjectOfType<ActiveLevelInfo>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            ActiveLevelManager.FinishedLoading += LevelLoaded;
        }

        private void OnDisable()
        {
            ActiveLevelManager.FinishedLoading -= LevelLoaded;
        }

        private void Update()
        {
            xMove = Input.GetAxisRaw("Horizontal");

            if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
                jumped = true;
        }

        private void FixedUpdate() 
        {
            if(jumped)
            {
                onJumped?.Invoke();
                jumped = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            rb.velocity = new Vector2(xMove * movementSpeed, rb.velocity.y); 
        }

        private void LateUpdate()
        {
            animator.SetBool("isMoving", xMove != 0);

            Vector2 scale = transform.localScale;
            if(xMove > 0)
                scale.x = -1;
            else if(xMove < 0)
                scale.x = 1;

            transform.localScale = scale;
        }
        #endregion

        #region //Level interface methods
        public void LevelLoaded()
        {
            transform.position = levelInfo.GetSpawnPoint();
        }

        public void OnResetLevel()
        {
            rb.velocity = Vector2.zero;
            jumped = false;
        }
        #endregion
    }

}