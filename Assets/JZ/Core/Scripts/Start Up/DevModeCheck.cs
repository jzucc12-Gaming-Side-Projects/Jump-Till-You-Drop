using UnityEngine;

namespace JZ.CORE.STARTUP
{
    public class DevModeCheck : MonoBehaviour
    {
        [SerializeField] private bool matchDev = true;

        private void Awake() 
        {
            gameObject.SetActive(!(GameSettings.InDevMode() ^ matchDev));    
        }
    }
}