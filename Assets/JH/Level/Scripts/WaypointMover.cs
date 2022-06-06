using UnityEngine;

namespace JH.LEVEL
{
    public class WaypointMover : MonoBehaviour
    {
        #region //Movement
        [SerializeField] private float moveSpeed = .03f;
        [SerializeField] private float distanceToSwitchWaypoints = 0.1f;
        [SerializeField] private bool flipSprite = true;
        private Vector2 movementVector = Vector2.zero;
        #endregion

        #region //Waypoints
        [SerializeField] private Transform startPoint = null;
        [SerializeField] private Transform[] waypoints = new Transform[0];
        private int waypointIndex = 0;
        private Transform currentWaypoint => waypoints[waypointIndex];
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            startPoint.parent = transform.parent;
            
            foreach(var waypoint in waypoints)
                waypoint.parent = transform.parent;
        }

        private void Start()
        {
            transform.position = startPoint.position;
        }

        private void OnDisable()
        {
            ResetMover();
        }

        private void FixedUpdate()
        {
            MoverObject();
            CheckForNextWaypoint();
        }

        private void LateUpdate()
        {
            if(!flipSprite) return;
            Vector2 scale = transform.localScale;
            if(movementVector.x < 0)
                scale.x = 1;
            else if(movementVector.x > 0)
                scale.x = -1;
            transform.localScale = scale;

        }
        #endregion

        #region //Movement
        private void MoverObject()
        {
            Vector2 startPosition = transform.position;
            Vector2 newPosition = Vector2.MoveTowards(startPosition, currentWaypoint.position, moveSpeed);
            movementVector = newPosition - startPosition;
            transform.position = newPosition;
        }

        private void CheckForNextWaypoint()
        {
            float distanceToCurrentWaypoint = Vector2.Distance(transform.position, currentWaypoint.position);
            if(distanceToCurrentWaypoint > distanceToSwitchWaypoints) return;

            waypointIndex++;
            if(waypointIndex >= waypoints.Length)
                waypointIndex = 0;
        }

        private void ResetMover()
        {
            transform.position = startPoint.position;
            waypointIndex = 0;
        }
        #endregion
    
        public Vector2 GetMovementVector()
        {
            return movementVector;
        }
    }
}
