using UnityEngine;
using System.IO;
using System;

public class SaveManager : Singleton<SaveManager>
{

    [SerializeField] private string path = Application.streamingAssetsPath + "/save.json";
    [SerializeField] private SaveSetup _saveSetup;
    public int lastLevel;
    public Action<SaveSetup> FileLoaded;
    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != this) return;   // duplicata vinda de outra cena sera destruida
        transform.SetParent(null);      // DontDestroyOnLoad exige objeto raiz
        DontDestroyOnLoad(gameObject);
        Load();                         // sincrono: pronto antes de todos os Start()
    }

    void Start()
    {
        if (Instance != this) return;
        FileLoaded?.Invoke(_saveSetup);   // evita NRE ao dar play direto na fase (sem inscritos)
    }

    void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Limao";
    }

    [NaughtyAttributes.Button]
    void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = CollectableManager.Instance.GetItemByType(CollectableType.COIN).coins.value;
        _saveSetup.health = CollectableManager.Instance.GetItemByType(CollectableType.LIFE_PACK).coins.value;
        Save();
    }

    public void SaveProgress(int checkpoint)
    {
        _saveSetup.lastCheckpoint = checkpoint;
        _saveSetup.playerLife = Player.Instance.healthBase.CurrentLife;
        _saveSetup.clothType = Player.Instance.CurrentClothType;
        SaveItems();   // ja grava coins + life packs e chama Save()
    }

    void SaveFile(string json)
    {
        Debug.Log("Saving..." + json);
        File.WriteAllText(path, json);
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        Save();
    }

    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }

    public void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

    [NaughtyAttributes.Button]
    void Load()
    {
        _saveSetup = null;
        if (File.Exists(path))
        {
            try { _saveSetup = JsonUtility.FromJson<SaveSetup>(File.ReadAllText(path)); }
            catch (Exception e) { Debug.LogWarning("Save invalido, criando um novo: " + e.Message); }
        }

        if (_saveSetup == null)   // arquivo ausente, corrompido ou JSON "null"
        {
            CreateNewSave();
            Save();
        }

        lastLevel = _saveSetup.lastLevel;
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int coins;
    public int health;

    public string playerName;

    public int lastCheckpoint = 0;
    public float playerLife = -1f;  // -1 = nunca salvo
    public int clothType = -1;      // -1 = roupa padrao
}
