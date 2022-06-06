using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.INPUT
{
    public class ControlDisplay : MonoBehaviour
    {
        #region//Cached variables
        [SerializeField] Image controlImage = null;
        [SerializeField] TextMeshProUGUI controlText = null;
        [SerializeField] bool liveUpdate = false;
        [SerializeField] string preface = "";
        #endregion

        #region//Other
        public bool isController = false;
        GamepadType myType = GamepadType.xbox;
        #endregion

        #region//Controls
        [SerializeField] Control computerControl = new Control();
        [SerializeField] Control xboxControl = new Control();
        [SerializeField] Control sonyControl = new Control();
        [SerializeField] Control switchControl = new Control();
        #endregion


        #region//Monobehaviour
        private void OnEnable() 
        {
            myType = DeviceChecker.CurrentGamepad();
            UpdateControl();
        }

        private void LateUpdate()
        {
            if(liveUpdate && isController != DeviceChecker.isUsingGamepad)
            {
                isController = DeviceChecker.isUsingGamepad;
                myType = DeviceChecker.CurrentGamepad();
                UpdateControl();
            }

            if (isController && myType != DeviceChecker.CurrentGamepad())
            {
                myType = DeviceChecker.CurrentGamepad();
                UpdateControl();
            }
        }
        #endregion

        #region//Update display
        public void UpdateControl()
        {
            Hide();

            if (!isController)
                UpdateDisplay(computerControl);
            else
            {
                switch (DeviceChecker.CurrentGamepad())
                {
                    case GamepadType.sony:
                        UpdateDisplay(sonyControl);
                        break;
                    case GamepadType.nSwitch:
                        UpdateDisplay(switchControl);
                        break;
                    case GamepadType.xbox:
                    case GamepadType.none:
                    default:
                        UpdateDisplay(xboxControl);
                        break;
                }
            }
        }

        void UpdateDisplay(Control _control)
        {
            if (string.IsNullOrEmpty(_control.controlName))
                ShowImage(_control);
            else
                ShowText(_control);
        }

        void ShowImage(Control _control)
        {
            if(!string.IsNullOrEmpty(preface))
            {
                controlText.gameObject.SetActive(true);
                controlText.text = SetupPreface();
                controlText.color = _control.controlColor;
                controlText.fontSize = _control.fontSize;
            }

            controlImage.color = _control.controlColor;
            controlImage.sprite = _control.controlSprite;
            controlImage.enabled = true;
            controlImage.gameObject.SetActive(true);
        }

        void ShowText(Control _control)
        {
            controlText.gameObject.SetActive(true);
            controlText.text = $"{SetupPreface()}{_control.controlName}";
            controlText.color = _control.controlColor;
            controlText.fontSize = _control.fontSize;
        }

        public void Hide()
        {
            controlImage.gameObject.SetActive(false);
            controlText.gameObject.SetActive(false);
        }

        string SetupPreface()
        {
            if(string.IsNullOrEmpty(preface)) return "";
            return $"<color=white>{preface}</color>";
        }
        #endregion
    }

    [Serializable]
    public struct Control
    {
        public string controlName;
        public Color controlColor;
        public Sprite controlSprite;
        public int fontSize;
    }
}