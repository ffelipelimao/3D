using DG.Tweening;
using UnityEngine;

public class CharacterStateJump : StateBase
{
    private readonly Character _owner;
    private Tween _tween;

    public CharacterStateJump(Character owner)
    {
        _owner = owner;
    }

    public override void OnStateEnter(object o = null)
    {
        Debug.Log("[Character] Enter JUMP");

        _tween = _owner.transform
            .DOJump(_owner.transform.position, _owner.jumpHeight, 1, _owner.jumpDuration)
            .OnComplete(() => _owner.SwitchState(Character.CharacterStates.STOP));
    }

    public override void OnStateStay() { }

    public override void OnStateExit()
    {
        Debug.Log("[Character] Exit JUMP");
        if (_tween != null && _tween.IsActive()) _tween.Kill();
    }
}
