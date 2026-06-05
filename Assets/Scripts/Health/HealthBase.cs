using UnityEngine;
using System;
using Unity.VisualScripting;

public class HealthBase : MonoBehaviour
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;
    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;


    void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {

        if (destroyOnKill) Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void DoDamage()
    {
        Damage(5f);
    }

    public void Damage(float f)
    {
        //  transform.position -= transform.forward;

        _currentLife -= f;
        if (_currentLife <= 0)
        {
            Kill();
        }

        OnDamage?.Invoke(this);
    }

}
