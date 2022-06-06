using UnityEngine;

namespace JZ.UI.MENU.MEMBER
{
    public abstract class MenuMemberBehavior : MonoBehaviour
    {
        MenuMember myMember;

        protected virtual void Awake() 
        {
            myMember = GetComponent<MenuMember>();    
            OnHover(false);    
        }

        protected virtual void OnEnable() 
        {
            myMember.MemberHovered += OnHover;
        }

        protected virtual void OnDisable() 
        {
            myMember.MemberHovered -= OnHover;
        }

        protected virtual void OnHover(bool _isHovering) { }
        protected virtual void OnSelect() { }
    }
}
