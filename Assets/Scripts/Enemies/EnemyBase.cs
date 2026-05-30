using UnityEngine;
using DG.Tweening;
using Animation;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;
    public float startAnimationDuration = 1.4f;
    public bool startWithAnimation = true;
    public Ease ease = Ease.OutBack;
    [SerializeField] private AnimationBase _animationBase;
    public Collider coll;
    public VFXFlashColor flashColor;
    public ParticleSystem pSystem;

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        ResetLife();
        if (startWithAnimation) StartAnimationBorn();
    }

    void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        OnKill();
    }

    protected virtual void OnKill()
    {
        if (coll != null) coll.enabled = false;
        Destroy(gameObject, 3f);
        PlayAnimationByTrigger(AnimationType.DEATH);
    }

    protected virtual void OnDamage(float f)
    {
        if (flashColor != null) flashColor.Flash();
        if (pSystem != null) pSystem.Emit(15);
        _currentLife -= f;
        if (_currentLife <= 0)
        {
            Kill();
        }
    }

    void StartAnimationBorn()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(ease).From();
    }

    public void PlayAnimationByTrigger(AnimationType animationType)
    {
        _animationBase.PlayAnimationTrigger(animationType);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnDamage(5f);
        }
    }

    public void Damage(float damage)
    {
        Debug.Log("Damage");
        OnDamage(damage);
    }
}
