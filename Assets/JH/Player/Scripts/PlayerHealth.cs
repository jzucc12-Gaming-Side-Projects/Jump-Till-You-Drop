using System;
using JH.LEVEL;
using JZ.AUDIO;
using UnityEngine;

namespace JH.PLAYER
{
    public class PlayerHealth : MonoBehaviour, IOnResetLevel
    {
        #region //Cached components
        [SerializeField] private SpriteRenderer playerSprite = null;
        private PlayerMovement playerMovement = null;
        private GroundChecker groundChecker = null;
        private SoundPlayer sfxPlayer = null;
        private ParticleSystem deathParticles = null;
        private BoxCollider2D hitcollider = null;
        #endregion

        #region //Health variables
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private bool invincible = false;
        private int currentHealth = 3;
        private const int healthCap = 3;
        #endregion

        public event Action<string> OnPlayerDied;
        public event Action<int> OnPlayerHealthChange;
        public event Action<int> AddedNewHealth;


        #region //Monobehaviour
        private void Awake() 
        {
            playerMovement = GetComponent<PlayerMovement>();
            groundChecker = GetComponent<GroundChecker>();
            sfxPlayer = GetComponent<SoundPlayer>();
            deathParticles = GetComponent<ParticleSystem>();
            hitcollider = GetComponent<BoxCollider2D>();
        }

        private void Start() 
        {
            SetHealth(maxHealth);
        }

        private void OnEnable()
        {
            playerMovement.onJumped += PlayerJumped;
            groundChecker.OnLanded += PlayerHeal;
            ActiveLevelManager.FinishedLoading += RevivePlayer;
        }

        private void OnDisable()
        {
            playerMovement.onJumped -= PlayerJumped;
            groundChecker.OnLanded -= PlayerHeal;
            ActiveLevelManager.FinishedLoading -= RevivePlayer;
        }
        #endregion

        #region //Health alteration
        //Public
        public void IncreaseMaxHealth()
        {
            SetHealth(maxHealth);
            maxHealth = Mathf.Min(maxHealth + 1, healthCap);
            AddedNewHealth?.Invoke(maxHealth);
        }

        public void PlayerHeal()
        {
            SetHealth(maxHealth);
        }

        //Private
        private void PlayerJumped()
        {
            SetHealth(currentHealth - 1);
            if(currentHealth < 0)
            {
                KillPlayer("JUMPED TILL YOU DROPPED!");
            }
            else
            {
                sfxPlayer.Play($"Jump {currentHealth}");
            }
        }

        private void SetHealth(int _newHealth)
        {
            currentHealth = _newHealth;
            OnPlayerHealthChange?.Invoke(currentHealth);
        }
        #endregion

        #region //Player death and revival
        public void KillPlayer(string _deathText)
        {
            if(invincible) return;
            deathParticles.Play();
            sfxPlayer.Play("Death");
            OnPlayerDied?.Invoke(_deathText);
            FindObjectOfType<LevelInterfaceCaller>().EndLevel();
            playerSprite.enabled = false;
            hitcollider.enabled = true;
        }

        private void RevivePlayer()
        {
            invincible = false;
            deathParticles.Stop();
            SetHealth(maxHealth);
            playerSprite.enabled = true;
            hitcollider.enabled = true;
        }

        public void OnResetLevel()
        {
            invincible = true;
            playerSprite.enabled = false;
            hitcollider.enabled = false;
        }
        #endregion
    }
}
