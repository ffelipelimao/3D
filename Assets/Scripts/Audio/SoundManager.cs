using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> SFXSetups;
    public AudioSource musicSource;

    private const string MutePrefKey = "SOUND_MUTED";
    public bool IsMuted { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IsMuted = PlayerPrefs.GetInt(MutePrefKey, 0) == 1;
        ApplyMute();
    }

    public void ToggleMute()
    {
        SetMuted(!IsMuted);
    }

    public void SetMuted(bool muted)
    {
        IsMuted = muted;
        PlayerPrefs.SetInt(MutePrefKey, muted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyMute();
    }

    private void ApplyMute()
    {
        AudioListener.volume = IsMuted ? 0f : 1f;
    }

    public void PlayMusicByType(MusicType musicType)
    {
        var type = musicSetups.Find(i => i.musicType == musicType);
        musicSource.clip = type.audio;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }
    public void PlaySFXByType(SFXType sfxType)
    {
        var type = SFXSetups.Find(i => i.sfxType == sfxType);
        musicSource.clip = type.audio;
        musicSource.Play();
    }

    public SFXSetup GetSFXByType(SFXType sFXType)
    {
        return SFXSetups.Find(i => i.sfxType == sFXType);
    }
}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03,
}
[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audio;
}

public enum SFXType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03,
    CHEST_OPEN,
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audio;
}