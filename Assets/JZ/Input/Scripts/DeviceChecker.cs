using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
#if !UNITY_WEBGL
using UnityEngine.InputSystem.Switch;
#endif
using UnityEngine.InputSystem.Users;

public class DeviceChecker : MonoBehaviour
{
    private PlayerInput playerInput;
    public static bool isUsingGamepad = false;
    private static GamepadType lastType = GamepadType.none;


    #region //Monobehaviour
    private void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();    
    }

    private void OnEnable()
    {
        InputUser.onChange += DeviceCheck;
    }

    private void OnDisable()
    {
        InputUser.onChange -= DeviceCheck;
    }
    #endregion

    public static GamepadType CurrentGamepad()
    {
        if(!isUsingGamepad)
            return lastType;
        if (Gamepad.current is DualShockGamepad)
            lastType = GamepadType.sony;
        #if !UNITY_WEBGL
        else if (Gamepad.current is SwitchProControllerHID)
            lastType = GamepadType.nSwitch;
        #endif
        else
            lastType = GamepadType.xbox;

        return lastType;
    }
    private void DeviceCheck(InputUser user, InputUserChange change, InputDevice dvc)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            isUsingGamepad = playerInput.currentControlScheme == "Gamepad";
        }
    }
}
