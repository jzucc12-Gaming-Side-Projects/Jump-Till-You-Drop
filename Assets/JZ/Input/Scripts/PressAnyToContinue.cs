using UnityEngine;
using UnityEngine.UI;

namespace JZ.INPUT
{
    public class PressAnyToContinue : MonoBehaviour
    {
        void Update()
        {
            if(JZ.INPUT.Utils.AnyKeyOrButton())
            {
                FindObjectOfType<Button>().onClick?.Invoke();
                return;
            }
        }
    }
}
