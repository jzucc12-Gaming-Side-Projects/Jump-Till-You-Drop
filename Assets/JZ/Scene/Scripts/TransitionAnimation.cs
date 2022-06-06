using System;
using UnityEngine;

namespace JZ.SCENE
{
    public class TransitionAnimation : MonoBehaviour
    {
        public event Action OnTransitionFinished;

        public void TransitionFinished()
        {
            OnTransitionFinished?.Invoke();
        }
    }
}
