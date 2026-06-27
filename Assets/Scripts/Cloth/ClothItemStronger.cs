using UnityEngine;

public class ClothItemStronger : ClothItemBase
{
    public float targetDefense = 2f;

    public override void Collect()
    {
        base.Collect();
        Player.Instance.healthBase.ChangeDamageMultiply(duration);
    }

}
