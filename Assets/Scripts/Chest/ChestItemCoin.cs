using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coin;

    public Vector2 randomRange = new Vector2(-0.5f, 0.5f);

    private List<GameObject> listCoins = new List<GameObject>();

    public float TweenEndTime = 0.5f;

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    [NaughtyAttributes.Button]
    public void CreateItems()
    {
        for (int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coin);
            item.transform.position = transform.position +

            Vector3.forward * Random.Range(randomRange.x, randomRange.y) +
            Vector3.right * Random.Range(randomRange.x, randomRange.y);

            item.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
            listCoins.Add(item);

        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach (var item in listCoins)
        {
            item.transform.DOKill();
            item.transform.DOMoveY(2f, TweenEndTime).SetRelative();
            item.transform.DOScale(0, TweenEndTime / 2)
                .SetDelay(TweenEndTime / 2)
                .OnComplete(() => Destroy(item));
            CollectableManager.Instance.AddByType(CollectableType.COIN);
        }
        listCoins.Clear();
    }

}


