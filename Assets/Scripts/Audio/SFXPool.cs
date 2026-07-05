using System;
using System.Collections.Generic;
using UnityEngine;

public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourceList;
    public int poolSize = 10;
    private int _index = 0;


    void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            AudioSourceItem();
        }
    }

    void AudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sFXType)
    {
        if (sFXType == SFXType.NONE) return;
        if (_audioSourceList == null || _audioSourceList.Count == 0) return;

        var sfx = SoundManager.Instance != null ? SoundManager.Instance.GetSFXByType(sFXType) : null;
        if (sfx == null || sfx.audio == null) return;

        _audioSourceList[_index].clip = sfx.audio;
        _audioSourceList[_index].Play();

        _index++;

        if (_index >= _audioSourceList.Count)
        {
            _index = 0;
        }
    }
}
