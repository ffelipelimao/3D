using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckpoint = 0;
    public List<CheckPointBase> checkPointBases;

    public bool HasCheckpoint()
    {
        return lastCheckpoint > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if (lastCheckpoint < i)
        {
            lastCheckpoint = i;
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkPointBases.Find(i => i.key == lastCheckpoint);

        return checkpoint.transform.position;
    }
}
