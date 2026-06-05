using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStateWalkForward : StateBase
{
    private readonly Character _owner;

    public CharacterStateWalkForward(Character owner)
    {
        _owner = owner;
    }

    public override void OnStateEnter(params object[] objects)
    {
        Debug.Log("[Character] Enter WALK_FORWARD");
    }

    public override void OnStateStay()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb.spaceKey.wasPressedThisFrame)
        {
            _owner.SwitchState(Character.CharacterStates.JUMP);
            return;
        }

        bool walking = kb.wKey.isPressed || kb.upArrowKey.isPressed;
        if (!walking)
        {
            _owner.SwitchState(Character.CharacterStates.STOP);
            return;
        }

        _owner.transform.Translate(Vector3.forward * _owner.walkSpeed * Time.deltaTime, Space.Self);
    }

    public override void OnStateExit()
    {
        Debug.Log("[Character] Exit WALK_FORWARD");
    }
}
