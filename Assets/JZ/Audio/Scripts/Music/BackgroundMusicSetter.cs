using UnityEngine;

namespace JZ.AUDIO.MUSIC
{
    public class BackgroundMusicSetter : MonoBehaviour
    {
        [SerializeField] private AudioClip backgroundTrack = null;
        [SerializeField] private bool autoSet = true;
        private BackgroundMusicPlayer bgPlayer = null;

        
        private void Start()
        {
            bgPlayer = FindObjectOfType<BackgroundMusicPlayer>();
            if(autoSet) SetTrack();
        }

        public void SetTrack()
        {
            if (bgPlayer.HasClip("Background Music", backgroundTrack)) return;
            bgPlayer.ChangeBackgroundTrack("Background Music", backgroundTrack);
        }

        public void OverridePlayer(bool _lockIn)
        {
            bgPlayer.SetLock(false);
            SetTrack();
            bgPlayer.SetLock(_lockIn);
        }
        public bool IsPlayingMine() => bgPlayer.GetSound("Background Music").clip == backgroundTrack;
    }
}
