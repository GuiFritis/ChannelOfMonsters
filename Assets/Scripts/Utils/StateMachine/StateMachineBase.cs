using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.StateMachine{
    public class StateMachineBase<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> statesDictionary = new();

        private StateBase _currentState;
        private T _stateEnum;
        public float timeToStartGame = 1f;

        public StateBase CurrentState
        {
            get {return _currentState;}
        }

        public T StateEnum
        {
            get {return _stateEnum;}
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            statesDictionary.Add(typeEnum, state);
        }    

        public void SwitchState(T state, params object[] objs)
        {
            _currentState?.OnStateExit();

            _stateEnum = state;
            _currentState = statesDictionary[state];

            _currentState.OnStateEnter(objs);
        }

        public void Update()
        {
            _currentState?.OnStateStay();
        }

    }
}
