using System.Collections.Generic;
using UnityEngine;

public enum ClothType
{
    SPEED,
    STRONGER
}

public class ClothManager : Singleton<ClothManager>
{
    public List<ClothSetup> clothSetups;
    public ClothSetup GetClothSetupByType(ClothType clothType)
    {
        return clothSetups.Find(i => i.clothType == clothType);
    }
}

[System.Serializable]
public class ClothSetup
{
    public ClothType clothType;
    public Texture2D texture;
}
