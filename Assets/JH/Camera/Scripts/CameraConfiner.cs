using UnityEngine;
using JH.LEVEL;

namespace JH.CAMERA
{
    public class CameraConfiner : MonoBehaviour, ISubLevelComponentOnEnter
    {
        private PolygonCollider2D polyCollider = null;


        private void Awake() 
        {
            polyCollider = GetComponent<PolygonCollider2D>();
        }

        public void OnEnteringSubLevel()
        {
            if(!FindObjectOfType<PlayerCamera>()) return;
            FindObjectOfType<PlayerCamera>().ConfineVirtualCamera(polyCollider);
        }
    }
}
