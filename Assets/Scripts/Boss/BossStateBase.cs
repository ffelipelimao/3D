using UnityEngine;

public class BossStateBase : StateBase
{

    protected BossBase bossBase;


    public override void OnStateEnter(params object[] objects)
    {
        base.OnStateEnter(objects);
        bossBase = (BossBase)objects[0];
    }
}

public class BossStateInit : BossStateBase
{

    public override void OnStateEnter(params object[] objects)
    {
        base.OnStateEnter(objects);
        // Toca a animação de surgimento e, ao terminar, começa a andar/atacar.
        bossBase.StartInitAnimation(() => bossBase.SwitchState(BossAction.WALK));
    }
}

public class BossStateWalk : BossStateBase
{

    public override void OnStateEnter(params object[] objects)
    {
        base.OnStateEnter(objects);
        bossBase.GoToRandomPoint(OnArrive);
        Debug.Log("bbbb");
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        bossBase.StopAllCoroutines();
    }

    void OnArrive()
    {
        bossBase.SwitchState(BossAction.ATTACK);
    }
}

public class BossStateAttack : BossStateBase
{

    public override void OnStateEnter(params object[] objects)
    {
        base.OnStateEnter(objects);
        bossBase.StartAttack(EndAttack);
        Debug.Log("ccccc");
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        bossBase.StopAllCoroutines();
    }

    void EndAttack()
    {
        bossBase.SwitchState(BossAction.WALK);
    }
}

public class BossStateDeath : BossStateBase
{

    public override void OnStateEnter(params object[] objects)
    {
        base.OnStateEnter(objects);
        bossBase.transform.localScale = Vector3.one * .2f;
        Debug.Log("ddd");
    }
}