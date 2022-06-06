using JH.LEVEL;
using UnityEngine;
using TMPro;

namespace JH.UI
{
    public class EndGameMessage : MonoBehaviour, IOnEndLevel
    {
        public void OnStartLevel()
        {
            GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        }

        public void OnEndLevel()
        {
            GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
    }
}
