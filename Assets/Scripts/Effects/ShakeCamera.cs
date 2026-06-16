using UnityEngine;
using Unity.Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineCamera virtualCamera;

    public float shakeTime;

    [Header("Shake Values")]
    public float amplitude = 1f;
    public float frequency = 1f;
    public float time = .1f;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(1, 1, 1);
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        if (!virtualCamera.isActiveAndEnabled) return;

        virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = amplitude;
        virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = 0f;
            virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = 0f;
        }
    }
}
