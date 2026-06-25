using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    public float shakeDuration = 0.1f;
    public int shakeForce = 2;

    public int dropCoinsAmount = 10;
    public GameObject coinPrefab;
    public Transform dropPosition;


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
        DropCoins();
    }

    [NaughtyAttributes.Button]
    void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.transform.position;
        i.transform.DOScale(0, 2f).SetEase(Ease.OutBack).From();
    }

    [NaughtyAttributes.Button]
    void DropGroupOfCoin()
    {
        StartCoroutine(DropCoinsDelayed());
    }

    IEnumerator DropCoinsDelayed()
    {
        for (int i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
