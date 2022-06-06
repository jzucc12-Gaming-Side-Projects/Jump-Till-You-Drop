using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


//From Game Dev Guide
namespace JZ.CUTSCENE.SUBTITLE
{
    [TrackBindingType(typeof(TextMeshProUGUI))]
    [TrackClipType(typeof(SubtitleClip))]
    public class SubtitleTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<SubtitleTrackMixer>.Create(graph,inputCount);
        }
    }
}
