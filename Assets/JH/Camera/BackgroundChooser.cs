using UnityEngine;

namespace JH.LEVEL
{
    public class BackgroundChooser : MonoBehaviour
    {
        [SerializeField] private GameObject[] backgrounds = new GameObject[0];
        private int currentIndex = -1;
        private GameObject activeBackground => backgrounds[currentIndex];


        public void ChooseBackground(string _sceneName)
        {
            string firstNumber = _sceneName.ToCharArray()[0].ToString();

            int num;
            HideBackground();
            if(int.TryParse(firstNumber, out num))
            {
                currentIndex = num - 1;
                activeBackground.SetActive(true);
            }
            else
            {
                currentIndex = -1;
            }

        }

        public void HideBackground()
        {
            if(currentIndex >= 0)
                activeBackground.SetActive(false);
        }
    }
}