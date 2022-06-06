using UnityEngine;

namespace JZ.UI.MENU
{
    public class SubMenu : MonoBehaviour
    {
        [SerializeField] private MenuUI parentMenu = null;
        [SerializeField] private GameObject blockingWindow = null;


        private void OnEnable()
        {
            blockingWindow.SetActive(true);
            parentMenu.LockControl(true);
        }

        private void OnDisable()
        {
            parentMenu.LockControl(false);
            blockingWindow.SetActive(false);
        }
    }
}
