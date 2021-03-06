using System;
using JZ.AUDIO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JZ.UI.MENU.MEMBER
{
    public class MenuMember : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region //Variables
        public event Action<MenuMember> PointerEnter;
        public event Action<bool> MemberHovered;
        private SoundPlayer sfxPlayer;
        #endregion

        #region //Monobehaviour
        protected virtual void Awake() { }
        protected virtual void Start() 
        {
            Hover(false);    
        }
        #endregion

        #region //Event Methods
        public void Hover(bool _active) 
        { 
            MemberHovered?.Invoke(_active);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerEnter?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        #endregion
    }
}