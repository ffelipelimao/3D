using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    public float shakeDuration = 0.1f;
    public int shakeForce = 2;


    void OnValidate()
    {
        if (healthBase != null) healthBase = GetComponent<HealthBase>();
    }

    void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    void OnDamage(HealthBase hb)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up / 2, shakeForce);
    }
}
