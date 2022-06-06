using UnityEngine;

namespace JZ.BUTTONS.FUNCTION
{
    public class QuitGameButton : ButtonFunction
    {
        public override void OnClick()
        {
            Application.Quit();
        }
    }
}