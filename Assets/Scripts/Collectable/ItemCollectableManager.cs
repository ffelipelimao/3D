using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableManager : MonoBehaviour
{
    public CollactableLayout collactableLayout;
    public Transform container;
    public List<CollactableLayout> collactableLayouts;

    void Start()
    {
        CreateItems();
    }

    void CreateItems()
    {
        foreach (var setup in CollectableManager.Instance.collectableSetups)
        {
            var item = Instantiate(collactableLayout, container);
            item.Load(setup);
            collactableLayouts.Add(item);
        }
    }

}
