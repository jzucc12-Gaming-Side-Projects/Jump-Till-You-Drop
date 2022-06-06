using System.Linq;
using UnityEngine;

namespace JH.LEVEL
{
    public class LevelInterfaceCaller : MonoBehaviour
    {
        public void ResetLevel()
        { 
            var elements = FindObjectsOfType<MonoBehaviour>().OfType<IOnResetLevel>();
            foreach(var element in elements)
                element.OnResetLevel();

            StartLevel();
        }

        public void StartLevel()
        {
            var elements = FindObjectsOfType<MonoBehaviour>().OfType<IOnStartLevel>();
            foreach(var element in elements)
                element.OnStartLevel();
        }

        public void EndLevel()
        {
            var elements = FindObjectsOfType<MonoBehaviour>().OfType<IOnEndLevel>();
            foreach(var element in elements)
                element.OnEndLevel();
        }
    }
}