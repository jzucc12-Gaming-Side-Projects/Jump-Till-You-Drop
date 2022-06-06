using System;
using UnityEngine;

namespace JZ.CORE.STARTUP
{
    public class GameStartUp : MonoBehaviour
    {
        [SerializeField] private int frameRate = 60;
        [SerializeField] private bool runInBackground = true;
        public static event Action SetUpDone;

        private void Awake()
        {
            Application.targetFrameRate = frameRate;
            Application.runInBackground = runInBackground;
            InitiateVolume();
            SetUpDone?.Invoke();
        }

        private void InitiateVolume()
        {
            if(!PlayerPrefs.HasKey(GameSettings.masterVolKey))
            {
                PlayerPrefs.SetFloat(GameSettings.masterVolKey, 0.5f);
                PlayerPrefs.SetFloat(GameSettings.musicVolKey, 0.5f);
                PlayerPrefs.SetFloat(GameSettings.sfxVolKey, 0.5f);
            }

            GameSettings.SetVolume(VolumeType.master, PlayerPrefs.GetFloat(GameSettings.masterVolKey));
            GameSettings.SetVolume(VolumeType.music, PlayerPrefs.GetFloat(GameSettings.musicVolKey));
            GameSettings.SetVolume(VolumeType.sfx, PlayerPrefs.GetFloat(GameSettings.sfxVolKey));
        }
    }
}
