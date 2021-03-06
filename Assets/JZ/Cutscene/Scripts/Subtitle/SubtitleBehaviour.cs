using UnityEngine;
using UnityEngine.Playables;


//From Game Dev Guide
namespace JZ.CUTSCENE.SUBTITLE
{
    public class SubtitleBehaviour : PlayableBehaviour
    {
        public string subtitleText = "";
        public Color subtitleColor = Color.white;
        public bool keepBetweenClips = false;
    }
}