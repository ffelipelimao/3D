using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;

public enum BossAction
{
    INIT,
    IDLE,
    WALK,
    ATTACK,
    DEATH,

}
public class BossBase : MonoBehaviour
{
    public float animationDuration = 0.5f;
    private StateMachine<BossAction> stateMachine;

    public float speed = 5f;
    public List<Transform> wayPoints;

    public int attackAmount = 4;
    public float timeBetweenAttacks = 0.5f;

    public HealthBase healthBase;

    void Awake()
    {
        Init();
        healthBase.OnKill += OnBoosKill;
    }

    // Ponto de entrada chamado pelo SpawnTrigger: faz o chefão aparecer e começar a agir.
    [NaughtyAttributes.Button]
    public void StartBoss()
    {
        SwitchState(BossAction.INIT);
    }

    void Init()
    {
        stateMachine = new StateMachine<BossAction>();
        stateMachine.Init();
        stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
        stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
        stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
    }

    [NaughtyAttributes.Button]
    private void SwitchInit()
    {
        SwitchState(BossAction.INIT);
    }

    [NaughtyAttributes.Button]
    private void SwitchWalk()
    {
        SwitchState(BossAction.WALK);
    }
    [NaughtyAttributes.Button]
    private void SwitchAttack()
    {
        SwitchState(BossAction.ATTACK);
    }


    public void SwitchState(BossAction bossState)
    {
        stateMachine.SwitchState(bossState, this);
    }

    public void StartInitAnimation(Action onComplete = null)
    {
        transform.DOScale(0, animationDuration).SetEase(Ease.OutBack).From()
            .OnComplete(() => onComplete?.Invoke());
    }

    public void GoToRandomPoint(Action onArrive = null)
    {
        StartCoroutine(GoPointCoroutine(wayPoints[UnityEngine.Random.Range(0, wayPoints.Count)], onArrive));
    }

    IEnumerator GoPointCoroutine(Transform t, Action onArrive = null)
    {
        while (Vector3.Distance(transform.position, t.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }

        onArrive?.Invoke();

    }

    public void StartAttack(Action endCallback = null)
    {
        StartCoroutine(StartAttackCoroutine(endCallback));
    }

    IEnumerator StartAttackCoroutine(Action endCallback = null)
    {
        int attacks = 0;
        while (attacks < attackAmount)
        {
            attacks++;
            transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        endCallback?.Invoke();
    }

    void OnBoosKill(HealthBase b)
    {
        stateMachine.SwitchState(BossAction.DEATH);
    }
}
