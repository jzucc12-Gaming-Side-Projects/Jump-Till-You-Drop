using JZ.BUTTONS.FUNCTION;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JZ.SCENE.BUTTON
{
    public abstract class SceneButtonFunction : ButtonFunction
    {
        [SerializeField] private AnimType animationType = AnimType.fade;
        private bool animateTransition => animationType != AnimType.none;

        public override void OnClick()
        {
            if(animateTransition)
            {
                if(UseIndex()) 
                    FindObjectOfType<SceneTransitioner>().TransitionToScene(TargetSceneIndex(), animationType);
                else
                    FindObjectOfType<SceneTransitioner>().TransitionToScene(TargetSceneName(), animationType);
            }
            else
            {
                if(UseIndex()) 
                    SceneManager.LoadScene(TargetSceneIndex());
                else
                    SceneManager.LoadScene(TargetSceneName());
            }
        }

        private bool UseIndex() => TargetSceneIndex() != -1;
        protected virtual int TargetSceneIndex() { return -1; }
        protected virtual string TargetSceneName() { return ""; }
    }
}