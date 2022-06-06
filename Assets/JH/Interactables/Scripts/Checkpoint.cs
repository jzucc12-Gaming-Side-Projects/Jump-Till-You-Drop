using UnityEngine;
using JH.LEVEL;
using JH.PLAYER;
using System;
using JZ.AUDIO;

namespace JH.INTERACTABLES
{
    public class Checkpoint : MonoBehaviour
    {
        #region //Checkpoint variables
        [SerializeField] private Material activeMaterial = null;
        private Material inactiveMaterial = null;
        private SpriteRenderer spriteRenderer = null;

        private static event Action CheckpointActivated;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            inactiveMaterial = spriteRenderer.material;
        }

        private void OnEnable()
        {
            CheckpointActivated += DeactivateCheckpoint;
            if(GetIsActiveCheckpoint())
                ActivateCheckpoint();
            else
                DeactivateCheckpoint();
        }

        private void OnDisable()
        {
            CheckpointActivated -= DeactivateCheckpoint;
        }

        private void Start()
        {
            if(GetIsActiveCheckpoint())
                ActivateCheckpoint();
            else
                DeactivateCheckpoint();
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.GetComponent<PlayerHealth>() == null) return;
            SetCheckpointVariables(); 
            ActivateCheckpoint();
            CheckpointActivated?.Invoke();
        }
        #endregion

        #region //Activation
        private void DeactivateCheckpoint()
        {
            if(GetIsActiveCheckpoint()) return;
            spriteRenderer.material = inactiveMaterial;
        }

        private void ActivateCheckpoint()
        {
            spriteRenderer.material = activeMaterial;
        }

        private void SetCheckpointVariables()
        {
            if(GetIsActiveCheckpoint()) return;
            GetComponent<SoundPlayer>().Play("Checkpoint");
            FindObjectOfType<ActiveLevelInfo>().SetSpawnPoint(transform.position);
            FindObjectOfType<ActiveLevelInfo>().SetRootScene(gameObject.scene.name);
        }

        private bool GetIsActiveCheckpoint()
        {
            return FindObjectOfType<ActiveLevelInfo>().GetRootScene() == gameObject.scene.name;
        }
        #endregion
    }
}