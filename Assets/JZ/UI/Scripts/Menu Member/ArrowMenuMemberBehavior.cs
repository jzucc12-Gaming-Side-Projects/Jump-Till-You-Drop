using UnityEngine;
using UnityEngine.UI;

namespace JZ.UI.MENU.MEMBER
{
    public class ArrowMenuMemberBehavior : MenuMemberBehavior
    {
        [SerializeField] private Image arrowImage = null;
        private Color arrowColor = Color.clear;


        protected override void Awake() 
        {
            arrowColor = arrowImage.color;
            base.Awake();
        }

        protected override void OnHover(bool _active)
        {
            Color newColor = _active ? arrowColor : Color.clear;
            arrowImage.color = newColor;
        }
    }
}