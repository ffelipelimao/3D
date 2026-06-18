using UnityEngine;

public class ActionLifepack : MonoBehaviour
{
    public ScriptableObjectInt scriptableObjectInt;


    void Start()
    {
        scriptableObjectInt = CollectableManager.Instance.GetItemByType(CollectableType.LIFE_PACK).coins;
    }

    void RestoreLife()
    {
        if (scriptableObjectInt.value > 0)
        {
            CollectableManager.Instance.RemoveByType(CollectableType.LIFE_PACK);
            Player.Instance.healthBase.ResetLife();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            RestoreLife();
        }
    }
}
