using UnityEngine;

public class Character : Singleton<Character>
{
    public enum CharacterStates
    {
        STOP,
        WALK_FORWARD,
        JUMP
    }

    [Header("Movement")]
    public float walkSpeed = 3f;
    public float jumpHeight = 2f;
    public float jumpDuration = 0.6f;

    public StateMachine<CharacterStates> stateMachine;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<CharacterStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(CharacterStates.STOP, new CharacterStateStop(this));
        stateMachine.RegisterStates(CharacterStates.WALK_FORWARD, new CharacterStateWalkForward(this));
        stateMachine.RegisterStates(CharacterStates.JUMP, new CharacterStateJump(this));
        stateMachine.SwitchState(CharacterStates.STOP);
    }

    void Update()
    {
        if (stateMachine != null) stateMachine.Update();
    }

    public void SwitchState(CharacterStates state)
    {
        stateMachine.SwitchState(state);
    }
}
