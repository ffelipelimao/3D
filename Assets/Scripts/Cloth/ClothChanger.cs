using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Texture2D texture;
    public string ShaderIDName = "_EmissionMap";
    private Texture2D _defaultTexture;

    void Awake()
    {
        _defaultTexture = (Texture2D)skinnedMeshRenderer.materials[0].GetTexture(ShaderIDName);
    }

    [NaughtyAttributes.Button]
    void ChangeTexture()
    {
        skinnedMeshRenderer.materials[0].SetTexture(ShaderIDName, texture);
    }

    public void ChangeTexture(ClothSetup setup)
    {
        skinnedMeshRenderer.materials[0].SetTexture(ShaderIDName, setup.texture);
    }

    public void ResetTexture()
    {
        skinnedMeshRenderer.materials[0].SetTexture(ShaderIDName, _defaultTexture);
    }
}