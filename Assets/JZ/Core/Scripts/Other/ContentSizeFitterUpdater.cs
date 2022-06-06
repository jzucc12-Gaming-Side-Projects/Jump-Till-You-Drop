using UnityEngine;
using UnityEngine.UI;

namespace JZ.CORE
{
    public class ContentSizeFitterUpdater : MonoBehaviour
    {
        private void Start()
        {
            UpdateContentSizeFitter();
        }

        public void UpdateContentSizeFitter()
        {
            Canvas.ForceUpdateCanvases();
            foreach(var layout in GetComponentsInChildren<VerticalLayoutGroup>())
            {
                layout.enabled = false;
                layout.enabled = true;
            }

            foreach(var layout in GetComponentsInChildren<HorizontalLayoutGroup>())
            {
                layout.enabled = false;
                layout.enabled = true;
            }
        }
    }
}