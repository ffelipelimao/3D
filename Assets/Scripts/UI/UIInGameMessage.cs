using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIInGameMessage : Singleton<UIInGameMessage>
{
    public Text messageText;
    public float defaultDuration = 2f;
    public float fadeDuration = 0.3f;
    private Sequence _currentSequence;

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        if (messageText.font == null)
            messageText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        var color = messageText.color;
        color.a = 0;
        messageText.color = color;
    }

    void OnValidate()
    {
        if (messageText == null) messageText = GetComponent<Text>();
    }

    public void ShowMessage(string message)
    {
        ShowMessage(message, defaultDuration);
    }

    public void ShowMessage(string message, float duration)
    {
        if (_currentSequence != null) _currentSequence.Kill();

        messageText.text = message;

        _currentSequence = DOTween.Sequence()
            .Append(messageText.DOFade(1f, fadeDuration))
            .AppendInterval(duration)
            .Append(messageText.DOFade(0f, fadeDuration));
    }
}
