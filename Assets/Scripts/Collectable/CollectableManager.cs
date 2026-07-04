using System.Collections.Generic;
using UnityEngine;


public enum CollectableType
{
    COIN,
    LIFE_PACK
}
public class CollectableManager : Singleton<CollectableManager>
{

    public List<CollectableSetup> collectableSetups;

    void Start()
    {
        Reset();
        LoadItemsFromSave();
    }
    void Reset()
    {
        foreach (var i in collectableSetups)
        {
            i.coins.value = 0;
        }
    }

    void LoadItemsFromSave()
    {
        AddByType(CollectableType.COIN, SaveManager.Instance.Setup.coins);
        AddByType(CollectableType.LIFE_PACK, SaveManager.Instance.Setup.health);
    }

    public void AddByType(CollectableType collectableType, int amount = 1)
    {
        if (amount < 0) return;
        collectableSetups.Find(i => i.collectableType == collectableType).coins.value += amount;
    }

    public CollectableSetup GetItemByType(CollectableType collectableType)
    {
        return collectableSetups.Find(i => i.collectableType == collectableType);
    }

    public void RemoveByType(CollectableType collectableType, int amount = 1)
    {
        var collectables = collectableSetups.Find(i => i.collectableType == collectableType);
        collectables.coins.value -= amount;
        if (collectables.coins.value < 0) collectables.coins.value = 0;
    }

    [NaughtyAttributes.Button]
    private void AddCoin()
    {
        AddByType(CollectableType.COIN);
    }

    [NaughtyAttributes.Button]
    private void AddLifePack()
    {
        AddByType(CollectableType.LIFE_PACK);
    }

}

[System.Serializable]
public class CollectableSetup
{
    public ScriptableObjectInt coins;
    public CollectableType collectableType;
    public Sprite Icon;

}
