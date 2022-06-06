using System.Collections;
using JZ.AUDIO;
using TMPro;
using UnityEngine;

namespace  JZ.DIALOGUE
{
    public class TextPrinter : MonoBehaviour
    {
        #region //Cached Components
        [SerializeField] private TextMeshProUGUI myText = null;
        private SoundPlayer sfxManager = null;
        #endregion

        #region //Print Variables
        [SerializeField] private int charPerSecond = 10;
        private string finalText = "";
        private const string printSFXName = "Text Print";
        #endregion


        #region //Monobheaviour
        private void Awake() 
        {
            sfxManager = GetComponent<SoundPlayer>();
        }
        #endregion

        #region //Getters
        public string GetFinalText() { return finalText; }
        public bool IsPrinting() { return myText.text != finalText; }
        #endregion

        #region //Character printing
        //Public
        public void PrintNewText(string _newText)
        {   
            if(string.IsNullOrEmpty(_newText))
            {
                Debug.LogWarning(name + ": Tried to print null or empty text");
                return;
            }
            if(IsPrinting()) StopPrinting(false);

            finalText = _newText;
            myText.text = "";
            StartCoroutine(PrintRoutine());
        }

        public void StopPrinting(bool _stopInPlace)
        {
            StopAllCoroutines();
            if(sfxManager != null) sfxManager.Stop(printSFXName);
            if(!_stopInPlace) myText.text = finalText;
            finalText = myText.text;
        }

        //Private
        private IEnumerator PrintRoutine()
        {
            int characterRate = Application.targetFrameRate / charPerSecond;
            int frameNo = 0;
            int charCount = 1;

            myText.text = finalText.Substring(0,charCount);
            int framesPerCharacter = Mathf.Max(characterRate, 1);
            if(sfxManager != null) sfxManager.Play(printSFXName);

            while(IsPrinting())
            {
                yield return new WaitUntil(() => 
                {
                    frameNo++;
                    return frameNo % framesPerCharacter != 0;
                });
                charCount++;
                charCount = CompensateForMarkup(charCount);
                string currentDisplay = finalText.Substring(0, charCount);
                myText.text = currentDisplay;
            }

            StopPrinting(true);
        }
        
        private int CompensateForMarkup(int _charCount)
        {
            if(_charCount > finalText.Length - 1) return _charCount;

            int start = 2;
            int searchDepth = 30;

            if (finalText[_charCount] == '<')
            {
                for (int ii = start; ii < searchDepth; ii++)
                {
                    if(_charCount + ii > finalText.Length - 1) return _charCount;
                    if (finalText[_charCount + ii] != '>') continue;
                    return CompensateForMarkup(_charCount + ii + 1);
                }
            }

            return _charCount;
        }
        #endregion
    }
}
