using UnityEngine;

namespace JZ.SCENE.BUTTON
{
    public class SceneChangeButton : SceneButtonFunction
    {
        [SerializeField] string targetScene;
        protected override string TargetSceneName() => targetScene;
    }
}