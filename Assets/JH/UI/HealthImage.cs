using System;
using UnityEngine;
using UnityEngine.UI;

namespace JH.UI
{
    public class HealthImage : MonoBehaviour
    {
        private Image healthImage = null;
        private Animator myAnimator = null;
        private static event Action ReSync;


        #region //Monobehaviour
        private void Awake()
        {
            healthImage = GetComponent<Image>();
            myAnimator = GetComponent<Animator>();
        }

        private void OnEnable() 
        {
            ReSync += SyncAnimations;
        }

        private void OnDisable() 
        {
            ReSync -= SyncAnimations;
        }
        #endregion

        #region //Visibility
        //Public
        public void ChangeImageState(bool _show)
        {
            myAnimator.enabled = false;
            healthImage.enabled = _show;
            myAnimator.enabled = true;
        }

        public void Grow()
        {
            ChangeImageState(false);
            myAnimator.SetTrigger("Grow");
        }

        public void Show()
        {
            healthImage.enabled = true;
        }

        public void SetAnimationPosition()
        {
            ReSync?.Invoke();
        }

        //Private
        private void SyncAnimations()
        {
            myAnimator.StopPlayback();
            myAnimator.Play("Player Health Animation", -1, 0);
        }
        #endregion
    }
}
