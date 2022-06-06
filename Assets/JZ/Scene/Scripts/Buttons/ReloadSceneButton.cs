using UnityEngine.SceneManagement;

namespace JZ.SCENE.BUTTON
{
    public class ReloadSceneButton : SceneButtonFunction
    {
        protected override int TargetSceneIndex() => SceneManager.GetActiveScene().buildIndex;
    }
}