using UnityEngine;

namespace JZ.CORE.STARTUP
{
    public class HasPrerequisite : MonoBehaviour
    {
        [SerializeField] private bool ignoreInDevMode = true;

        protected void Check(bool _show)
        {
            if (ignoreInDevMode && GameSettings.InDevMode()) return;
            gameObject.SetActive(_show);
        }
    }

}