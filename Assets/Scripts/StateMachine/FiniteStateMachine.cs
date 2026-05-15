using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    public enum StateEnumExample
    {
        STATE_ONE,
        STATE_TWO,
        STATE_THREE,
    }

    public StateMachine<StateEnumExample> stateMachine;

    void Start()
    {
        stateMachine = new StateMachine<StateEnumExample>();
        stateMachine.Init();
        stateMachine.RegisterStates(StateEnumExample.STATE_ONE, new StateBase());
        stateMachine.RegisterStates(StateEnumExample.STATE_TWO, new StateBase());
    }
}
