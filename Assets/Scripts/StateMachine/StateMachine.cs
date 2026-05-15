using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Reflection;

public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictionaryStates;
    private StateBase _currentState;
    public float timeToStartGame = 1.0f;

    public StateBase CurrentState
    {
        get { return _currentState; }
    }

    public void Init()
    {
        dictionaryStates = new Dictionary<T, StateBase>();
    }

    public void RegisterStates(T typeEnum, StateBase stateBase)
    {
        dictionaryStates.Add(typeEnum, stateBase);

        //SwitchState(States.NONE);

        //Invoke(nameof(StartGame), timeToStartGame);
    }

    /*
    #if UNITY_EDITOR
        #region DEBUG

        [Button]
        private void ChangeStateToStateX()
        {
            SwitchState(States.NONE);
        }

        [Button]
        private void ChangeStateToStateY()
        {
            SwitchState(States.NONE);
        }

        #endregion
    #endif

    */
    public void SwitchState(T state)
    {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = dictionaryStates[state];
        _currentState.OnStateEnter();
    }

    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnStateStay();
        }
    }
}