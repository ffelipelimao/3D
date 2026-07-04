using System.Collections.Generic;
using UnityEngine;

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
            SaveManager.Instance.SaveProgress(lastCheckpoint);
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkPointBases.Find(i => i.key == lastCheckpoint);

        return checkpoint.transform.position;
    }

    void Start()
    {
        LoadFromSave();
    }

    void LoadFromSave()
    {
        if (SaveManager.Instance == null) return;
        lastCheckpoint = SaveManager.Instance.Setup.lastCheckpoint;
        if (!HasCheckpoint()) return;

        if (checkPointBases.Find(c => c.key == lastCheckpoint) == null)
        {
            lastCheckpoint = 0;   // save de outra fase / corrompido
            return;
        }

        foreach (var checkpoint in checkPointBases)
        {
            if (checkpoint.key <= lastCheckpoint) checkpoint.MarkAsActive();
        }

        Player.Instance.Respawn();

        if (UIInGameMessage.Instance != null)
            UIInGameMessage.Instance.ShowMessage("Progresso carregado");
    }
}
