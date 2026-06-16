using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class EffectsManager : Singleton<EffectsManager>
{
    public Volume processVolume;
    [SerializeField] private Vignette _vignette;
    public float duration = 1f;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        if (processVolume.profile.TryGet<Vignette>(out Vignette tmp))
        {
            _vignette = tmp;
        }

        float time = 0;
        while (time < duration)
        {
            _vignette.color.Override(Color.Lerp(Color.black, Color.red, time / duration));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < duration)
        {
            _vignette.color.Override(Color.Lerp(Color.red, Color.black, time / duration));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
