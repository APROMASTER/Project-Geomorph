using UnityEngine;

[ExecuteInEditMode]
public class SetRendererColor : MonoBehaviour
{
    [SerializeField] private string _colorPropertyName;
    [SerializeField] Color _colorValue;
    private MeshRenderer _renderer;
    
    private void Awake() 
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    void OnValidate()
    {
        SetColor(_colorValue);
    }

    public void SetColor(Color newColor)
    {
        if (_renderer == null || !_renderer.sharedMaterial.HasProperty(_colorPropertyName)) return;
        // MaterialPropertyBlock materialPropertyBlock = new();
        // materialPropertyBlock.SetColor(_colorPropertyName, newColor);
        // _renderer.SetPropertyBlock(materialPropertyBlock);
        _renderer.sharedMaterial.SetColor(_colorPropertyName, newColor);
    }
}
