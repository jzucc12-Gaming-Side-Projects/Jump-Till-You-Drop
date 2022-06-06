using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JZ.SCENE
{
    //Originally from Brackeys//
    public class SceneTransitioner : MonoBehaviour
    {
        #region //Animation Variables
        private TransitionAnimation[] animations = new TransitionAnimation[0];
        private List<Animator> animators = new List<Animator>();
        private Animator currentAnimator = null;
        #endregion

        #region //Transition Variables
        public static event Action StartTransitionOut;
        public static event Action StartTransitionIn;
        float preLoadBuffer = 0.25f;
        float postLoadBuffer = 0.25f;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            animations = GetComponentsInChildren<TransitionAnimation>(); 
            foreach(var animation in animations)
                animators.Add(animation.GetComponent<Animator>());
        }

        private void OnEnable() 
        {
            foreach(var animation in animations)
                animation.OnTransitionFinished += TransitionFinished;
        }

        private void OnDisable() 
        {
            foreach(var animation in animations)
                animation.OnTransitionFinished -= TransitionFinished;
        }
        #endregion

        #region //Transitions
        //Public
        public bool IsTransitioning()
        {
            return currentAnimator != null;
        }

        public void TransitionToScene(string _name, AnimType _type)
        {
            if(IsTransitioning()) return;
            currentAnimator = animators[(int)_type];
            int index = JZ.SCENE.Utils.GetSceneIndexFromName(_name);
            StartCoroutine(TransitionCoroutine(index));
        }

        public void TransitionToScene(int _sceneIndex, AnimType _type)
        {
            if(IsTransitioning()) return;
            currentAnimator = animators[(int)_type];
            StartCoroutine(TransitionCoroutine(_sceneIndex));
        }

        //Private
        private IEnumerator TransitionCoroutine(int _index)
        {
            //Transition out
            Time.timeScale = 1;
            currentAnimator.SetTrigger("TransitionOut");

            //Transition
            while(currentAnimator.GetCurrentAnimatorClipInfoCount(0) == 0) yield return null;
            yield return SceneManager.LoadSceneAsync(_index);
            float waitDuration = currentAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.averageDuration;
            yield return new WaitForSeconds(waitDuration + preLoadBuffer);
            yield return new WaitForSeconds(postLoadBuffer);

            //Transition in
            currentAnimator.SetTrigger("TransitionIn");
            StartTransitionIn?.Invoke();
        }

        private void TransitionFinished()
        {
            currentAnimator = null;
        }
        #endregion
    }
}
