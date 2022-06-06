using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT
{
    public class FullScreenSwitcher : MonoBehaviour
    {
        private GeneralInputs inputs = null;
        private void Awake() 
        {
            inputs = new GeneralInputs();
        }

        private void OnEnable() 
        {
            inputs.Map.ToggleFullscreen.Enable();  
            inputs.Map.ToggleFullscreen.started += ToggleFS;  
        }

        private void OnDisable() 
        {
            inputs.Map.ToggleFullscreen.Disable();    
            inputs.Map.ToggleFullscreen.started -= ToggleFS;  
        }

        private void ToggleFS(InputAction.CallbackContext _context)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

}