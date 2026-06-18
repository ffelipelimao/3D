using UnityEngine;


public class ItemCollactableCoin : ItemCollectableBase
{
    public Collider2D colly;
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddByType(CollectableType.COIN);
        coll.enabled = false;
    }
}
