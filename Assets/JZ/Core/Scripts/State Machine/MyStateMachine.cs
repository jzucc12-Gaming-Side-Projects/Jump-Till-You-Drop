using System;
using System.Collections;
using UnityEngine;

namespace JZ.CORE.STATEMACHINE
{
    public abstract class MyStateMachine<T> : MonoBehaviour where T : State 
    {
        #region//State Variables
        private T currentState = null;
        private T previousState = null;
        [SerializeField] private bool isLocked = false;
        public event Action<T> stateChanged;
        #endregion


        #region//Monobehaviour
        protected virtual void Awake() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        protected virtual void Start()
        {
            StateStartUp();
        }    

        protected virtual void Update()
        {
            currentState.StateUpdate();
        }
        #endregion

        #region //Getters
        public T GetCurrentState() { return currentState; }
        public T GetPreviousState() { return previousState; }
        public bool GetIsLocked() { return isLocked; }
        #endregion

        #region//Start Up
        protected abstract void StateStartUp();
        #endregion

        #region//State Logic
        //Methods return true if state successfully changed
        public bool ChangeState(T _newState)
        {
            if (currentState != null)
            {
                if (DontChangeState(_newState)) return false;
                currentState.EndState(_newState);
                previousState = currentState;
            }

            currentState = _newState;
            currentState.StartState();
            stateChanged?.Invoke(_newState);
            return true;
        }

        //Returns true if state should not change
        protected virtual bool DontChangeState(T _newState)
        {
            if (_newState == currentState) return true;
            if (isLocked) return true;
            return false;
        }
        #endregion

        #region//Other
        public void RunRoutine(IEnumerator _CR)
        {
            StartCoroutine(_CR);
        }
        #endregion
    }
}