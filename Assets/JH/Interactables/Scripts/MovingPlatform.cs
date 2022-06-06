using System.Collections.Generic;
using JH.LEVEL;
using UnityEngine;

namespace JH.INTERACTABLES
{
    public class MovingPlatform : MonoBehaviour
    {
        private WaypointMover mover = null;
        private List<Rigidbody2D> rbs = new List<Rigidbody2D>();


        private void Awake()
        {
            mover = GetComponent<WaypointMover>();
        }

        private void OnCollisionEnter2D(Collision2D other) 
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if(rb == null) return;
            rbs.Add(rb);
        }

        private void OnCollisionExit2D(Collision2D other) 
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if(rbs.Contains(rb))
                rbs.Remove(rb);
        }

        private void FixedUpdate()
        {
            foreach(var rb in rbs)
                rb.position += mover.GetMovementVector();
        }
    }
}
