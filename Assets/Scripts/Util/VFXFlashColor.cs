using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VFXFlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Renderer[] renderers;

    public Color color = Color.red;
    public float duration = 0.3f;
    private Tween _currentTween;
    private Renderer[] _targets;

    void Start()
    {
        BuildTargets();
    }

    void BuildTargets()
    {
        var list = new List<Renderer>();
        if (renderers != null) list.AddRange(renderers);
        if (meshRenderer) list.Add(meshRenderer);
        if (skinnedMeshRenderer) list.Add(skinnedMeshRenderer);
        _targets = list.ToArray();
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (_targets == null || _targets.Length == 0) BuildTargets();
        if (_targets.Length == 0 || _currentTween.IsActive()) return;

        var seq = DOTween.Sequence();
        foreach (var r in _targets)
        {
            if (r == null) continue;
            seq.Join(r.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo));
        }
        _currentTween = seq;
    }
}
