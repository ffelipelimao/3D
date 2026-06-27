using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;
    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public List<UI> ui;
    public float damageMultiplier = 1f;


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

        _currentLife -= f * damageMultiplier;
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

    public void ChangeDamageMultiply(float duration)
    {
        StartCoroutine(ChangeDamageMultiplyCoroutine(damageMultiplier, duration));
    }

    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiplier, float duration)
    {

        this.damageMultiplier = damageMultiplier;
        yield return new WaitForSeconds(duration);
        this.damageMultiplier = 1;
    }

}
