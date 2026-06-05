using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStateStop : StateBase
{
    private readonly Character _owner;

    public CharacterStateStop(Character owner)
    {
        _owner = owner;
    }

    public override void OnStateEnter(params object[] objects)
    {
        Debug.Log("[Character] Enter STOP");
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

        if (kb.wKey.isPressed || kb.upArrowKey.isPressed)
        {
            _owner.SwitchState(Character.CharacterStates.WALK_FORWARD);
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("[Character] Exit STOP");
    }
}
