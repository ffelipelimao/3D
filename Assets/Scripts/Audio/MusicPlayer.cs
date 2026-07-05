using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    public AudioSource audioSource;
    private MusicSetup _currentMusicSetup;

    void Start()
    {
        Play();
    }

    void Play()
    {
        _currentMusicSetup = SoundManager.Instance.GetMusicByType(musicType);
        audioSource.clip = _currentMusicSetup.audio;
        audioSource.Play();
    }
}
