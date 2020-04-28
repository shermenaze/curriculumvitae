using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private MeshRenderer _meshRenderer;
    
    private readonly Color _selectedColor = Color.green;
    private readonly Color _naturalColor = Color.black;

    private static readonly int RimColor = Shader.PropertyToID("_RimColor");

    private void Awake()
    {
        if(_meshRenderer) _meshRenderer.material.SetColor(RimColor, _naturalColor);
    }

    public virtual void Interact() { }

    public void Highlight(bool shouldHighlight)
    {
        if (!_meshRenderer) return;
        
        _meshRenderer.material.SetColor(RimColor,
            shouldHighlight ? _selectedColor : _naturalColor);
    }
}