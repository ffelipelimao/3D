using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;
    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public List<UI> ui;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
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

        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 direction)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (ui != null)
        {
            ui.ForEach(i => i.UpdateValue((float)_currentLife / startLife));

        }
    }
}
