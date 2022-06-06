using JZ.INPUT;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.UI
{
    public class ControlsScreenUI : MonoBehaviour
    {
        #region //Cached Components
        [SerializeField] Button toKeyboardButton = null;
        [SerializeField] Button toControllerButton = null;
        [SerializeField] GameObject keyboardScreen = null;
        [SerializeField] GameObject controllerScreen = null;
        MenuingInputSystem menuSystem;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            menuSystem = new MenuingInputSystem(new MenuingInputs());    
        }

        private void OnEnable() 
        {
            SwapScreen(false);
            menuSystem.Activate();
            GetComponent<Canvas>().enabled = true;
        }

        private void OnDisable() 
        {
            menuSystem.Deactivate();
            GetComponent<Canvas>().enabled = false;
        }

        private void Update()
        {
            if(menuSystem.GetXNav() > 0)
            {
                menuSystem.ExpendXDir();
                SwapScreen(true);
            }
            else if(menuSystem.GetXNav() < 0)
            {
                menuSystem.ExpendXDir();
                SwapScreen(false);
            }
        }
        #endregion

        public void SwapScreen(bool _toController)
        {
            controllerScreen.SetActive(_toController);
            keyboardScreen.SetActive(!_toController);
            toControllerButton.gameObject.SetActive(!_toController);
            toKeyboardButton.gameObject.SetActive(_toController);
        }
    }
}