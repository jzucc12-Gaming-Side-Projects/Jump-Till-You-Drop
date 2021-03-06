using UnityEngine;
using UnityEngine.UI;

namespace JZ.BUTTONS.FUNCTION
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonFunction : MonoBehaviour
    {
        #region//Monobehaviour
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        protected virtual void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClick);
        }
        #endregion

        #region//Pointer events
        public abstract void OnClick();
        #endregion
    }
}