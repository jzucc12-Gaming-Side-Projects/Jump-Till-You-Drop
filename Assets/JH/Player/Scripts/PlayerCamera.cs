using Cinemachine;
using JH.LEVEL;
using UnityEngine;

namespace JH.CAMERA
{
    public class PlayerCamera : MonoBehaviour
    {
        #region //Cached components
        private CinemachineVirtualCamera vCam = null;
        private CinemachineConfiner confiner = null;
        private ActiveLevelInfo levelInfo = null;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            levelInfo = FindObjectOfType<ActiveLevelInfo>();
            vCam = GetComponentInChildren<CinemachineVirtualCamera>();
            confiner = GetComponentInChildren<CinemachineConfiner>();
        }
        #endregion

        #region //Setters
        public void ConfineVirtualCamera(PolygonCollider2D _collider)
        {
            confiner.m_BoundingShape2D = _collider;
        }
        #endregion
    }
}