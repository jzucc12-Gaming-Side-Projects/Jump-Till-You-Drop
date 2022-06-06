using UnityEngine;

namespace JZ.CORE.STARTUP
{
    public class DevMode : MonoBehaviour
    {
        [SerializeField] private bool devMode = false;

        private void Awake()
        {
            int devModeValue = devMode ? 1 : 0;
            PlayerPrefs.SetInt(PlayerPrefKeys.devModeKey, devModeValue);
        }
    }
}
