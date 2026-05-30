using UnityEngine;
using DG.Tweening;

public class VFXFlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color color = Color.red;
    public float duration = 0.3f;
    private Color defaultColor;
    private Tween _currentTween;

    void Start()
    {
        defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (!_currentTween.IsActive())
        {
            _currentTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
