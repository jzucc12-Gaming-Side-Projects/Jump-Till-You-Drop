using UnityEngine;

public static class PlayerPrefKeys
{
    public const string devModeKey = "inDevMode";


    public static bool InDevMode()
    {
        int inDevMode = PlayerPrefs.GetInt(devModeKey, 0);
        return inDevMode == 1;
    }
}
