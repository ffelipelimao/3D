using UnityEngine;

public class TriggerMagneticsPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase i = other.transform.GetComponent<ItemCollectableBase>();
        if (i != null)
        {
            i.gameObject.AddComponent<Magnetics>();
        }
    }
}
