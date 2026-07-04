using UnityEngine;

public class ClothItemBase : MonoBehaviour
{
    public ClothType clothType;
    public string compareTag = "Player";
    public float duration = 2f;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    public virtual void Collect()
    {
        var setup = ClothManager.Instance.GetClothSetupByType(clothType);
        Player.Instance.EquipCloth(setup);
        HideObject();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

}
