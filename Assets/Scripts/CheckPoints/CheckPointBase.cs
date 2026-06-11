using UnityEngine;
using DG.Tweening;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 1;
    public string checkpointKey = "checkpoint_key";
    public bool checkpointActive = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Checkpoint();
        }
    }

    void Checkpoint()
    {
        TurnItOn();
        SaveCheckpoint();
    }

    [NaughtyAttributes.Button]
    void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.green);
    }

    [NaughtyAttributes.Button]
    void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.red);
    }

    void SaveCheckpoint()
    {
        /*if (PlayerPrefs.GetInt(checkpointKey) < key)
        {
            PlayerPrefs.SetInt(checkpointKey, key);
            checkpointActive = true;
        }
        */
        CheckPointManager.Instance.SaveCheckpoint(key);
    }
}
